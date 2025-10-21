
## Общая информация: 
Репозиторий представлен как демонстрация кода и его использования в игровом проекте. Не является актуальной версией игры.

### Игровой цикл

**Назначение.** Управляет сменой дня и ночи, распределяет игровые фазы и уведомляет остальные системы о переходах.

**Паттерны.** Оркестратор (`GameCycleOrchestrator`), внедрение зависимостей через Zenject.

**Ключевые скрипты.**
- [`Game Director`](Scripts/_Entry%20Point/Game%20Director.cs) — стартует игровой цикл и регистрирует сервисы сцены.
- [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs) — координирует смену фаз и рассылку событий.
- [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs) — изменяет время суток и триггерит визуальные эффекты.
- [`DayCounterView`](Scripts/Day%20night%20system/DayCounterView.cs) — отображает текущий день и реагирует на смену фаз.
- [`Stamina System`](Scripts/Stamina%20System/Stamina%20System.cs) — восстанавливает выносливость между циклами.
- [`Stamina View`](Scripts/Stamina%20System/Stamina%20View.cs) — отображает текущий уровень выносливости игрока.

_Здесь можно разместить гифку системы._

### Экономика и контракты

**Назначение.** Формирует и проверяет контракты, ведёт учёт валюты и обновляет связанные интерфейсы.

**Паттерны.** Сервисный слой (`ContractSystem`, `Wallet`), наблюдатель для UI, пул элементов интерфейса.

**Ключевые скрипты.**
- [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs) — хранит баланс игрока и обрабатывает поступления/расходы.
- [`Day Money view`](Scripts/Money%20and%20stats/Day%20Money%20view.cs) — визуализирует дневной доход.
- [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs) — проверяет выполнение контрактов и выдаёт награды.
- [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs) — управляет списком активных контрактов на панели.
- [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs) — отображает карточку контракта и подтверждение игроком.

_Здесь можно разместить гифку системы._

### Постройки

**Назначение.** Описывает строительство объектов, передачу ресурсов в сервис постройки и прогресс возведения на сцене.

**Паттерны.** оркестратор (`BuildingOrchestrator`), событийная модель для UI, пул анимаций ресурсов.

**Ключевые скрипты.**
- [`BuildingService`](Scripts/Building%20system/BuildingService.cs) — хранит конфигурации построек и выдаёт точки строительства.
- [`BuildingOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs) — распределяет ресурсы и синхронизирует прогресс.
- [`Builder`](Scripts/Building%20system/Builder.cs) — управляет этапами строительства конкретного объекта.
- [`BuildingData`](Scripts/Building%20system/BuildingData.cs) — задаёт параметры и стоимость построек.
- [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs) — отслеживает стадии постройки и триггерит завершение.
- [`BuildingView`](Scripts/Building%20system/BuildingView.cs) — обновляет визуальное представление строящегося объекта.
- [`BuildingItemView`](Scripts/Building%20system/UI/BuildingItemView.cs) — отображает необходимые для построек ресурсы в интерфейсе.
- [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs) — базовый класс для всех построек.
- [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs) — управляет атакующими действиями башни.
- [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs) — анимирует передачу ресурсов в строительный сервис.
- [`ArcherTower`](Scripts/Building%20system/Buildings/ArcherTower.cs), [`Barricade`](Scripts/Building%20system/Buildings/Barricade.cs), [`VillagerHome`](Scripts/Building%20system/Buildings/VillagerHome.cs), [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs) — конкретные реализации построек и их логика.

_Здесь можно разместить гифку системы._

### Инвентарь и крафт

**Назначение.** Объединяет взаимодействие со слотами, рецепты крафта и управление предметами в интерфейсе.

**Паттерны.** наблюдатель для синхронизации UI, стратегия распределения предметов, посредник перетаскивания.

**Ключевые скрипты.**
- [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs) — задаёт общие операции со слотами и инвентарём.
- [`Inventory Service`](Scripts/Inventory%20System/Inventory%20Service.cs) — управляет сеткой инвентаря игрока и проверками доступности предметов.
- [`ChestService`](Scripts/Interactable%20Object/ChestService.cs) — расширяет слотовую систему для сундуков.
- [`Craft Cell Service`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs) — контролирует ячейки рецептов.
- [`Craft Service`](Scripts/Craft%20system/Craft%20Service.cs) — применяет рецепты и списывает ресурсы.
- [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs) — обрабатывает события перетаскивания мышью.
- [`CellView`](Scripts/Drag%20and%20drop/CellView.cs) — отображает состояние отдельного слота.
- [`Craft Result Cell`](Scripts/Drag%20and%20drop/Craft%20Result%20Cell.cs) — показывает результат рецепта.
- [`Drag Item`](Scripts/Drag%20and%20drop/Drag%20Item.cs) — визуализирует перемещаемый предмет.
- [`Grid Controller`](Scripts/Drag%20and%20drop/Grid%20Controller.cs) — управляет регистрацией слотов в сервисе.
- [`Inventory Grid Controller`](Scripts/Inventory%20System/Inventory%20Grid%20Controller.cs) — связывает инвентарь с UI сеткой.

_Здесь можно разместить гифку системы._

### Поселенцы

**Назначение.** Управляет созданием, назначением работ и отображением информации о жителях.

**Паттерны.** Внедрение зависимостей (Zenject), пул контроллеров персонажей.

**Ключевые скрипты.**
- [`NpcSystem`](Scripts/NPC/NpcSystem.cs) — распределяет задачи и контролирует статусы поселенцев.
- [`NpcService`](Scripts/NPC/NpcService.cs) — предоставляет данные и сервисы для других систем.
- [`Npc Manager`](Scripts/NPC/Npc%20Manager.cs) — создаёт и управляет контроллерами персонажей на сцене.
- [`VillagerData`](Scripts/NPC/VillagerData.cs) — содержит параметры и характеристики жителей.
- [`Villager Controller`](Scripts/NPC/Villager%20Controller.cs) — отвечает за поведение NPC в мире.
- [`Villager View`](Scripts/NPC/Villager%20View.cs) — визуализирует состояние персонажа и его анимации.
- [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs) — предоставляет точку взаимодействия игрока с управлением жителями.
- [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs) — синхронизирует UI панели управления поселенцами.
- [`ManagePanelView`](Scripts/NPC/UI/ManagePanelView.cs) — отображает список жителей и их статусы.
- [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs) — показывает подробности конкретного жителя.

_Здесь можно разместить гифку системы._

### Добыча ресурсов

**Назначение.** Управляет источниками ресурсов, процессом добычи и передачей предметов в [`Inventory Service`](Scripts/Inventory%20System/Inventory%20Service.cs).

**Ключевые скрипты.**
- [`MiningSytem`](Scripts/Mining%20System/MiningSytem.cs) — запускает добычу и списывает выносливость.
- [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs) — описывает конкретный источник ресурсов и его состояние.
- [`IResourceNode`](Scripts/Mining%20System/IResourceNode.cs) — контракт для взаимодействия с узлами добычи.
- [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs) — визуализирует полученные предметы.

_Здесь можно разместить гифку системы._

### Бой и оборона

**Назначение.** Формирует волны врагов, управляет их жизненным циклом и поддерживает оборонительные механики.

**Паттерны.** Объектный пул врагов, конечные автоматы состояний (`BaseEnemy`).

**Ключевые скрипты.**
- [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs) — координирует волны и их запуск.
- [`WaveService`](Scripts/Enemy%20system/WaveService.cs) — рассчитывает состав и расписание волн.
- [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs) — отвечает за спавн и очистку противников.
- [`EnemyWave`](Scripts/Enemy%20system/EnemyWave.cs) — хранит данные об отдельной волне.
- [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs) — базовое поведение врагов и переходы состояний.
- [`HealthView`](Scripts/Enemy%20system/HealthView.cs) — отображает здоровье противника.
- [`Skeleton`](Scripts/Enemy%20system/Skeleton.cs), [`Zombie`](Scripts/Enemy%20system/Zombie.cs) — конкретные типы врагов и их характеристики.

_Здесь можно разместить гифку системы._

### Игрок и взаимодействия

**Назначение.** Обеспечивает управление игроком, его анимации и взаимодействие с объектами окружения.

**Паттерны.** Посредник (`PlayerOrchestrator`), событийные уведомления интерактивных объектов.

**Ключевые скрипты.**
- [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs) — объединяет управление, анимации и взаимодействия.
- [`PlayerMovement`](Scripts/Player/PlayerMovement.cs) — реализует перемещение и физику игрока.
- [`PlayerView`](Scripts/Player/PlayerView.cs) — управляет анимациями персонажа.
- [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs) — отвечает за активацию интерактивных объектов.
- [`CameraFollow`](Scripts/Player/CameraFollow.cs) — ведёт камеру за игроком.
- [`Base Interactable Object`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs) — общая логика интерактивных объектов.
- [`Workbench`](Scripts/Interactable%20Object/Workbench.cs), [`Chest`](Scripts/Interactable%20Object/Chest.cs), [`Stall`](Scripts/Interactable%20Object/Stall.cs), [`Bed`](Scripts/Interactable%20Object/Bed.cs), [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs) — конкретные интерактивные объекты.
- [`ChestGirldController`](Scripts/Interactable%20Object/ChestGirldController.cs) — управляет отображением содержимого сундуков.
- [`InteractibleGlow`](Scripts/Interactable%20Object/InteractibleGlow.cs) — подсвечивает активные объекты при наведении.

_Здесь можно разместить гифку системы._

### Мир и визуал

**Назначение.** Отвечает за освещение, атмосферные эффекты и декоративные элементы сцены.

**Паттерны.** Tween-анимации (DOTween), объектный пул визуальных элементов, асинхронные коррутины.

**Ключевые скрипты.**
- [`Sprite View Controller`](Scripts/Sprite%20View%20Controller.cs) — управляет общими параметрами спрайтов.
- [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs) — плавно изменяет освещение объектов.
- [`Cloud Manager`](Scripts/Background/Cloud%20Manager.cs) — создаёт и обновляет облака.
- [`Cloud`](Scripts/Background/Cloud.cs) — описывает поведение отдельного облака.
- [`Tree view`](Scripts/Background/Tree%20view.cs) — контролирует отображение деревьев.

_Здесь можно разместить гифку системы._

>>>>>>> main
