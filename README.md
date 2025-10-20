diff --git a/README.md b/README.md
new file mode 100644
index 0000000000000000000000000000000000000000..51e2db99622c7cdc8f4fd893309fc292236e12b2
--- /dev/null
+++ b/README.md
@@ -0,0 +1,161 @@
+# Обзор игровых скриптов
+
+## Схема подсистем и ключевых классов
+
+- **Игровой цикл**
+  - [`GameDirector`](Scripts/_Entry%20Point/Game%20Director.cs)
+  - [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs)
+  - [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs)
+  - [`DayCounter`](Scripts/Day%20night%20system/DayCounter.cs)
+  - [`StaminaSystem`](Scripts/Stamina%20System/Stamina%20System.cs)
+  - [`StaminaView`](Scripts/Stamina%20System/Stamina%20View.cs)
+- **Экономика и контракты**
+  - [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs)
+  - [`DayMoneyView`](Scripts/Money%20and%20stats/Day%20Money%20view.cs)
+  - [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs)
+  - [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs)
+  - [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs)
+- **Постройки**
+  - [`BuildingService`](Scripts/Building%20system/BuildingService.cs)
+  - [`BuildingData`](Scripts/Building%20system/BuildingData.cs)
+  - [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs)
+  - [`BuildingsOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs)
+  - [`Builder`](Scripts/Building%20system/Builder.cs)
+  - [`BuildingView`](Scripts/Building%20system/BuildingView.cs)
+  - [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs)
+  - [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs) и наследники: [`ArcherTower`](Scripts/Building%20system/Buildings/ArcherTower.cs), [`VillagerHome`](Scripts/Building%20system/Buildings/VillagerHome.cs), [`Barricade`](Scripts/Building%20system/Buildings/Barricade.cs)
+- **Инвентарь и крафт**
+  - [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs)
+  - [`InventoryService`](Scripts/Inventory%20System/Inventory%20Service.cs)
+  - [`ChestService`](Scripts/Interactable%20Object/ChestService.cs)
+  - [`CraftCellService`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs)
+  - [`CraftService`](Scripts/Craft%20system/CraftService.cs)
+  - [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs)
+  - UI: [`CellView`](Scripts/Drag%20and%20drop/CellView.cs), [`CraftResultCell`](Scripts/Drag%20and%20drop/Craft%20Result%20Cell.cs), [`DragItem`](Scripts/Drag%20and%20drop/Drag%20Item.cs), [`CraftGridController`](Scripts/Drag%20and%20drop/Grid%20Controller.cs)
+- **Поселенцы**
+  - [`NpcSystem`](Scripts/NPC/NpcSystem.cs)
+  - [`NpcService`](Scripts/NPC/NpcService.cs)
+  - [`NpcManager`](Scripts/NPC/Npc%20Manager.cs)
+  - UI: [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs), [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs), [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs)
+- **Добыча ресурсов**
+  - [`MiningSystem`](Scripts/Mining%20System/MiningSytem.cs)
+  - [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs)
+  - [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs)
+- **Бой и оборона**
+  - [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs)
+  - [`WaveService`](Scripts/Enemy%20system/WaveService.cs)
+  - [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs)
+  - [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs) и производные
+  - [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs)
+- **Игрок и взаимодействия**
+  - [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs)
+  - [`PlayerMovement`](Scripts/Player/PlayerMovement.cs)
+  - [`PlayerView`](Scripts/Player/PlayerView.cs)
+  - [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs)
+  - [`BaseInteractableObject`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs) и наследники: [`Workbench`](Scripts/Interactable%20Object/Workbench.cs), [`Chest`](Scripts/Interactable%20Object/Chest.cs), [`Stall`](Scripts/Interactable%20Object/Stall.cs), [`Bed`](Scripts/Interactable%20Object/Bed.cs)
+  - [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs), [`TransferItem`](Scripts/Player/Resours%20transfer%20anim/TransferItem.cs)
+- **Мир и визуал**
+  - [`SpriteViewController`](Scripts/Sprite%20View%20Controller.cs)
+  - [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs)
+  - [`CloudManager`](Scripts/Background/Cloud%20Manager.cs)
+  - [`Cloud`](Scripts/Background/Cloud.cs)
+  - [`TreeView`](Scripts/Background/Tree%20view.cs)
+  - [`StiveView`](Scripts/Stive%20view.cs)
+
+## Краткие описания систем
+### Игровой цикл
+- **Отвечает за:** запуск дневного цикла, подготовку волн и синхронизацию подсистем дня/ночи, поселенцев, контрактов, выносливости и врагов.
+- **Взаимодействует с:** `GameCycleOrchestrator`, `DayNightSystem`, `DayCounter`, `StaminaSystem`, `NpcSystem`, `ContractSystem`, `EnemyOrchestrator`.
+- **Паттерны:** оркестратор, событийная модель.
+
+### Экономика и контракты
+- **Отвечает за:** генерацию заказов, проверку наличия ресурсов и выдачу наград.
+- **Взаимодействует с:** `ContractSystem`, `InventoryService`, `Wallet`, `ContractPanelController`, `ContractView`.
+- **Паттерны:** сервис, наблюдатель.
+
+### Постройки
+- **Отвечает за:** хранение состояния построек, автоматизированную доставку ресурсов и визуализацию прогресса.
+- **Взаимодействует с:** `BuildingService`, `BuildingsOrchestrator`, `ResourceTransferController`, `Builder`, `BuildingView`.
+- **Паттерны:** сервисы для состояния, событийные уведомления.
+
+### Инвентарь и крафт
+- **Отвечает за:** унифицированную работу со слотами, проверку рецептов и расход выносливости при создании предметов.
+- **Взаимодействует с:** `ServiceBase`, `InventoryService`, `ChestService`, `CraftCellService`, `DragDropOrchestrator`, `CraftService`.
+- **Паттерны:** шаблонный метод, наблюдатель.
+
+### Поселенцы
+- **Отвечает за:** хранение данных жителей, выдачу профессий и интерфейс управления.
+- **Взаимодействует с:** `NpcSystem`, `NpcService`, `NpcManager`, `ManageBoard`, `ManagePanelController`, `VillagerInfoView`, `MiningSystem`, `Wallet`.
+- **Паттерны:** внедрение зависимостей, событийные каналы.
+
+### Добыча ресурсов
+- **Отвечает за:** регистрацию точек добычи, списание выносливости и обработку результатов.
+- **Взаимодействует с:** `MiningSystem`, `ResourceNode`, `ChestService`, `ExtractedItemView`, `NpcSystem`, `StaminaSystem`.
+- **Паттерны:** наблюдатель, фасад.
+
+### Бой
+- **Отвечает за:** построение волн врагов, управление их жизненным циклом и взаимодействие с оборонительными постройками.
+- **Взаимодействует с:** `EnemyOrchestrator`, `WaveService`, `EnemyManager`, `BaseEnemy`, `ArcherTowerAttack`, `GameCycleOrchestrator`.
+- **Паттерны:** объектный пул, конечный автомат.
+
+### Игрок и взаимодействия
+- **Отвечает за:** управление перемещением и анимациями игрока, а также обработку взаимодействий с объектами сцены.
+- **Взаимодействует с:** `PlayerOrchestrator`, `PlayerMovement`, `PlayerView`, `PlayerInteraction`, `BaseInteractableObject`, `ResourceTransferController`.
+- **Паттерны:** медиатор, команда.
+
+### Мир и визуал
+- **Отвечает за:** изменение освещения сцены, настройку визуальных эффектов и управление фоновыми элементами.
+- **Взаимодействует с:** `DayNightSystem`, `SpriteDimmer`, `SpriteViewController`, `CloudManager`, `TreeView`, `StiveView`.
+- **Паттерны:** корутины, объектный пул, tween-анимации.
+## Детали по системам
+
+### Игровой цикл
+- [`GameDirector`](Scripts/_Entry%20Point/Game%20Director.cs) включает физику триггеров, подготавливает волны врагов и запускает асинхронный дневной цикл при старте сцены, делегируя работу [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs).
+- [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs) инициирует восстановление выносливости, спавн поселенцев, генерацию контрактов и призыв врагов после четвёртого дня, затем ждёт завершения цикла освещения, переводит систему в ночную фазу и проверяет завершение дня перед запуском следующего.
+- [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs) асинхронно изменяет освещение, уведомляя зарегистрированные [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs), а [`DayCounter`](Scripts/Day%20night%20system/DayCounter.cs) подписывается на счётчик дней и обновляет UI.
+- [`StaminaSystem`](Scripts/Stamina%20System/Stamina%20System.cs) хранит базовый запас выносливости, оповещает подписчиков об изменении и предотвращает действия при нехватке ресурса; [`StaminaView`](Scripts/Stamina%20System/Stamina%20View.cs) визуализирует оставшийся запас.
+- Все зависимости и синглтоны объявлены в [`GameInstaller`](Scripts/Zenject/Game%20Installer.cs), что подчёркивает использование Zenject для внедрения зависимостей.
+
+### Экономика и контракты
+- [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs) хранит счёт игрока и через событие `moneyChanged` уведомляет UI ([`DayMoneyView`](Scripts/Money%20and%20stats/Day%20Money%20view.cs)).
+- [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs) создаёт список заказов на основе доступных предметов и количества жителей, проверяет выполнение через `InventoryService` и пополняет кошелёк.
+- [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs) поддерживает пул карточек, а [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs) реализует удержание для завершения заказа, обращаясь к `ContractSystem`.
+
+### Постройки
+- [`BuildingService`](Scripts/Building%20system/BuildingService.cs) хранит метаданные построек и выдаёт точки спавна поселенцев, а [`BuildingData`](Scripts/Building%20system/BuildingData.cs) и [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs) отслеживают накопленные ресурсы.
+- [`BuildingsOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs) запрашивает ресурсы из `InventoryService`, проигрывает анимацию переноса через [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs) и обновляет UI с помощью событий `Builder.ItemAdded` (см. [`BuildingItemView`](Scripts/Building%20system/UI/BuildingItemView.cs)).
+- [`Builder`](Scripts/Building%20system/Builder.cs) управляет состоянием конкретного строения, инициирует сбор ресурсов, обновляет статус сервиса и восстанавливает здоровье построек, а [`BuildingView`](Scripts/Building%20system/BuildingView.cs) отображает прогресс с помощью DOTween.
+- [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs) и наследники вроде [`ArcherTower`](Scripts/Building%20system/Buildings/ArcherTower.cs), [`VillagerHome`](Scripts/Building%20system/Buildings/VillagerHome.cs) и [`Barricade`](Scripts/Building%20system/Buildings/Barricade.cs) инкапсулируют здоровье и жизненный цикл, позволяя подключать боевые компоненты, например [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs).
+
+### Инвентарь и крафт
+- [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs) предоставляет общий интерфейс слотов, реализуя проверки совместимости, вставку и извлечение с уведомлениями через событие `Changed`.
+- [`InventoryService`](Scripts/Inventory%20System/Inventory%20Service.cs) и [`ChestService`](Scripts/Interactable%20Object/ChestService.cs) расширяют базовый сервис, добавляя поиск подходящих ячеек и операции для строительства и контрактов.
+- [`CraftCellService`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs) управляет сеткой рецептов, а [`CraftService`](Scripts/Craft%20system/CraftService.cs) загружает адресуемые рецепты, сверяет содержимое сетки и расходует выносливость на создание результата.
+- [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs) интерпретирует клики мыши, перемещая стеки между `IStorageSlots` и `ICraftResultSlots`, а UI-компоненты ([`CellView`](Scripts/Drag%20and%20drop/CellView.cs), [`CraftResultCell`](Scripts/Drag%20and%20drop/Craft%20Result%20Cell.cs), [`DragItem`](Scripts/Drag%20and%20drop/Drag%20Item.cs), [`CraftGridController`](Scripts/Drag%20and%20drop/Grid%20Controller.cs)) подписываются на события сервиса.
+
+### Поселенцы
+- [`NpcService`](Scripts/NPC/NpcService.cs) хранит данные поселенцев, а [`NpcSystem`](Scripts/NPC/NpcSystem.cs) рассчитывает потребности, создаёт данные, вычитает стоимость разблокировки из кошелька и назначает работы.
+- [`NpcManager`](Scripts/NPC/Npc%20Manager.cs) создаёт и переиспользует контроллеры жителей, позиционируя их у домов и отправляя на работу, используя `MiningSystem` для определения рабочих точек.
+- [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs) открывает панель при наличии поселенцев, а [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs) и [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs) управляют UI разблокировки и назначения профессий через `NpcSystem`.
+
+### Добыча ресурсов
+- [`MiningSystem`](Scripts/Mining%20System/MiningSytem.cs) регистрирует узлы, списывает выносливость за ручную добычу и отправляет добычу рабочих в `ChestService`, а также предоставляет координаты работы.
+- [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs) запускает корутину добычи при взаимодействии игрока, проверяет вместимость инвентаря, уведомляет подписчиков через события `Tick`/`WorkerTick` и отображает добычу через [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs).
+
+### Бой и волны
+- [`WaveService`](Scripts/Enemy%20system/WaveService.cs) конструирует наборы врагов, [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs) вызывает нужную волну по номеру дня и делегирует создание и спавн [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs).
+- [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs) создаёт пул врагов по типам, настраивает сортировку слоёв и повторно активирует объекты при спавне, проверяя, остались ли живые противники.
+- [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs) реализует конечный автомат состояний движения и атаки, поиск цели через `Physics2D.Raycast`, а также обновление полоски здоровья.
+- Защитные здания, такие как [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs), используют очередь целей и стек стрел ([`Arrow`](Scripts/Prop/Arrow.cs)) для асинхронной стрельбы.
+
+### Игрок и взаимодействия
+- [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs) агрегирует движение, анимации и взаимодействия, блокируя перемещение во время активного взаимодействия; [`PlayerMovement`](Scripts/Player/PlayerMovement.cs) и [`PlayerView`](Scripts/Player/PlayerView.cs) отвечают за физику и визуал.
+- [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs) обрабатывает вход `E/Escape`, переключая состояние взаимодействия на основании `BaseInteractableObject.NeedStopPlayer`.
+- Наследники [`BaseInteractableObject`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs) включают [`Workbench`](Scripts/Interactable%20Object/Workbench.cs) (проверяет выносливость для открытия UI), [`Chest`](Scripts/Interactable%20Object/Chest.cs), [`Stall`](Scripts/Interactable%20Object/Stall.cs), [`Bed`](Scripts/Interactable%20Object/Bed.cs) (завершает день) и [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs), связывая игровые сервисы с игроком.
+- [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs) и [`TransferItem`](Scripts/Player/Resours%20transfer%20anim/TransferItem.cs) создают визуальные эффекты передачи предметов при строительстве.
+
+### Мир и визуал
+- [`SpriteViewController`](Scripts/Sprite%20View%20Controller.cs) управляет глобальным параметром шейдера и корректирует гамму всех зарегистрированных спрайтов, а [`TreeView`](Scripts/Background/Tree%20view.cs) и [`StiveView`](Scripts/Stive%20view.cs) регистрируют фоновые элементы.
+- [`CloudManager`](Scripts/Background/Cloud%20Manager.cs) и [`Cloud`](Scripts/Background/Cloud.cs) реализуют объектный пул и движение облаков, используя случайные параметры.
+- [`BuildingView`](Scripts/Building%20system/BuildingView.cs) и [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs) демонстрируют применение DOTween для анимации UI и объектов, а [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs) обеспечивает циклические эффекты освещения.
+
