using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
public class CraftService : IInitializable, ICraftResultSlots
{
    public event  Action<ItemStack> RecipeFound;
    public event Action<ItemStack> Changed;

    private StaminaSystem _stamina;
    private CraftingRecipe _currentRecipe;
    private CraftCellService _craftCellService;
    private const string RecipesLabel = "Recipe";
    private List<CraftingRecipe> allRecipes = new();
    private ItemStack[] _workbenchData = new ItemStack[9];
    
    [Inject]
    public void Construct(CraftCellService craftCellService,StaminaSystem staminaSystem) {
        _craftCellService = craftCellService;
        _stamina = staminaSystem;
    }

    public void Initialize()
    {
        LoadRecipes().Forget();
        _craftCellService.Changed += UpdateData;
    }

    public void TryTakeAll(out ItemStack stack) {
        if (_currentRecipe != null)
        {
            float craftPrice = _currentRecipe.Item.GetStaminaPrice;
            Debug.Log($"{_currentRecipe.Item} : {_currentRecipe.Item.GetStaminaPrice}");
            if (_stamina.TrySubtract(craftPrice))
            {
                stack = new(_currentRecipe.Item, _currentRecipe.Item.OutputCraftCount);
                _craftCellService.ExtractForCraft();
                CheckCraft();
            }
            else stack = null;
        }
        else {
            stack = null;
        }
    }
    private async UniTask LoadRecipes()
    {
        Debug.Log("[CraftController] Загрузка рецептов...");
        AsyncOperationHandle<IList<CraftingRecipe>> handle =
            Addressables.LoadAssetsAsync<CraftingRecipe>((object)RecipesLabel, null);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            allRecipes.Clear();
            allRecipes.AddRange(handle.Result);
            Debug.Log($"[CraftController] Загружено {allRecipes.Count} рецептов");
        }
        else
        {
            Debug.LogError($"[CraftController] Не удалось загрузить рецепты! Лейбл: {RecipesLabel}");
        }

    }

    private bool CheckCraft()
    {
        if (allRecipes.Count == 0)
        {
            Debug.Log("рецептов не найдено");
            _currentRecipe = null;
            RecipeFound?.Invoke(null);
            return false;
        }
        foreach (var recipe in allRecipes)
        {
            Debug.Log($"сравниваем текущий рецепт с {recipe.name}");
            if (IsMatch(recipe.Ingredients, _workbenchData))
            {
                _currentRecipe = recipe;
                RecipeFound?.Invoke(new ItemStack(recipe.Item, recipe.Item.OutputCraftCount));
                return true;
            }
        }
        _currentRecipe = null;
        RecipeFound?.Invoke(null);
        return false;
    }
    private bool IsMatch(ItemData[] recipeItems, ItemStack[] workbenchItems)
    {
        if (recipeItems == null || workbenchItems == null)
            return false;

        for (int i = 0; i < 9; i++)
        {
            ItemData recipeItem = recipeItems[i];
            ItemData workbenchItem =  workbenchItems[i] == null ? null : workbenchItems[i].Item;
            
            if ((recipeItem == null && workbenchItem != null) || (recipeItem != null && workbenchItem == null))
            {
                return false;
            }

            if (recipeItem != null && workbenchItem != null)
            {
                if (workbenchItem.name != recipeItem.name)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    private void UpdateData(int index, ItemStack stack) {
        _workbenchData[index] = stack;
        CheckCraft();
    }

}
