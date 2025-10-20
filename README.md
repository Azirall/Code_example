# Обзор игровых скриптов

## Карта подсистем
- **Игровой цикл**
  - [`Game Director`](Scripts/_Entry%20Point/Game%20Director.cs)
  - [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs)
  - [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs)
  - [`DayCounter`](Scripts/Day%20night%20system/DayCounter.cs)
  - [`Stamina System`](Scripts/Stamina%20System/Stamina%20System.cs)
  - [`Stamina View`](Scripts/Stamina%20System/Stamina%20View.cs)
- **Экономика и контракты**
  - [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs)
  - [`Day Money view`](Scripts/Money%20and%20stats/Day%20Money%20view.cs)
  - [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs)
  - [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs)
  - [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs)
- **Постройки**
  - [`BuildingService`](Scripts/Building%20system/BuildingService.cs)
  - [`BuildingOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs)
  - [`Builder`](Scripts/Building%20system/Builder.cs)
  - [`BuildingData`](Scripts/Building%20system/BuildingData.cs)
  - [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs)
  - [`BuildingView`](Scripts/Building%20system/BuildingView.cs)
  - [`BuildingItemView`](Scripts/Building%20system/UI/BuildingItemView.cs)
  - [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs) и производные ([`ArcherTower`](Scripts/Building%20system/Buildings/ArcherTower.cs), [`Barricade`](Scripts/Building%20system/Buildings/Barricade.cs), [`VillagerHome`](Scripts/Building%20system/Buildings/VillagerHome.cs), [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs))
  - [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs)
  - [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs)
- **Инвентарь и крафт**
  - [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs)
  - [`Inventory Service`](Scripts/Inventory%20System/Inventory%20Service.cs)
  - [`ChestService`](Scripts/Interactable%20Object/ChestService.cs)
  - [`Craft Cell Service`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs)
  - [`Craft Service`](Scripts/Craft%20system/Craft%20Service.cs)
  - [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs)
  - UI-компоненты: [`CellView`](Scripts/Drag%20and%20drop/CellView.cs), [`Craft Result Cell`](Scripts/Drag%20and%20drop/Craft%20Result%20Cell.cs), [`Drag Item`](Scripts/Drag%20and%20drop/Drag%20Item.cs), [`Grid Controller`](Scripts/Drag%20and%20drop/Grid%20Controller.cs), [`Inventory Grid Controller`](Scripts/Inventory%20System/Inventory%20Grid%20Controller.cs)
- **Поселенцы**
  - [`NpcSystem`](Scripts/NPC/NpcSystem.cs)
  - [`NpcService`](Scripts/NPC/NpcService.cs)
  - [`Npc Manager`](Scripts/NPC/Npc%20Manager.cs)
  - [`VillagerData`](Scripts/NPC/VillagerData.cs)
  - [`Villager Controller`](Scripts/NPC/Villager%20Controller.cs)
  - [`Villager View`](Scripts/NPC/Villager%20View.cs)
  - UI-компоненты: [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs), [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs), [`ManagePanelView`](Scripts/NPC/UI/ManagePanelView.cs), [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs)
- **Добыча ресурсов**
  - [`MiningSytem`](Scripts/Mining%20System/MiningSytem.cs)
  - [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs)
  - [`IResourceNode`](Scripts/Mining%20System/IResourceNode.cs)
  - [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs)
- **Бой и оборона**
  - [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs)
  - [`WaveService`](Scripts/Enemy%20system/WaveService.cs)
  - [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs)
  - [`EnemyWave`](Scripts/Enemy%20system/EnemyWave.cs)
  - [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs) и производные ([`Skeleton`](Scripts/Enemy%20system/Skeleton.cs), [`Zombie`](Scripts/Enemy%20system/Zombie.cs))
  - [`HealthView`](Scripts/Enemy%20system/HealthView.cs)
- **Игрок и взаимодействия**
  - [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs)
  - [`PlayerMovement`](Scripts/Player/PlayerMovement.cs)
  - [`PlayerView`](Scripts/Player/PlayerView.cs)
  - [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs)
  - [`CameraFollow`](Scripts/Player/CameraFollow.cs)
  - [`Base Interactable Object`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs) и наследники ([`Workbench`](Scripts/Interactable%20Object/Workbench.cs), [`Chest`](Scripts/Interactable%20Object/Chest.cs), [`Stall`](Scripts/Interactable%20Object/Stall.cs), [`Bed`](Scripts/Interactable%20Object/Bed.cs))
  - [`ChestGirldController`](Scripts/Interactable%20Object/ChestGirldController.cs)
  - [`InteractibleGlow`](Scripts/Interactable%20Object/InteractibleGlow.cs)
- **Мир и визуал**
  - [`Sprite View Controller`](Scripts/Sprite%20View%20Controller.cs)
  - [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs)
  - [`Cloud Manager`](Scripts/Background/Cloud%20Manager.cs)
  - [`Cloud`](Scripts/Background/Cloud.cs)
  - [`Tree view`](Scripts/Background/Tree%20view.cs)
  - [`Stive view`](Scripts/Stive%20view.cs)

## Краткие сведения о подсистемах
### Игровой цикл
- **Отвечает за:** запуск и завершение игровых дней, переключение фаз, восстановление ресурсов.
- **Взаимодействует с:** системами дня/ночи, выносливости, контрактов, поселенцев и обороны.
- **Паттерны:** оркестратор для центрального контроллера, события Unity для оповещения подписчиков, внедрение зависимостей через Zenject.

### Экономика и контракты
- **Отвечает за:** генерацию задач на поставку, проверку инвентаря и распределение наград.
- **Взаимодействует с:** `ContractSystem`, `Inventory Service`, `Wallet`, UI-панелями контрактов.
- **Паттерны:** сервисы для бизнес-логики, наблюдатель для обновления интерфейса, пул UI-элементов.

### Постройки
- **Отвечает за:** состояние построек, прогресс строительства, логистику доставки ресурсов и оборону.
- **Взаимодействует с:** `BuildingService`, оркестратором построек, переносом ресурсов, строительными представлениями и базовыми зданиями.
- **Паттерны:** сервисы данных, событийная модель для обновлений UI, объектный пул для анимаций переноса.

### Инвентарь и крафт
- **Отвечает за:** универсальные операции со слотами, применение рецептов и управление перетаскиванием.
- **Взаимодействует с:** сервисами инвентаря и сундуков, крафтовой сеткой, драг-н-дроп оркестратором и визуальными элементами.
- **Паттерны:** шаблонный метод в базовом сервисе слотов, наблюдатель для обновления UI, стратегия выбора количества предметов.

### Поселенцы
- **Отвечает за:** создание и управление жителями, назначение профессий, отображение их статусов.
- **Взаимодействует с:** сервисом поселенцев, менеджером, панелями управления, системой добычи и кошельком.
- **Паттерны:** внедрение зависимостей, событийные каналы между сервисами и UI, объектный пул контроллеров.

### Добыча ресурсов
- **Отвечает за:** регистрацию источников ресурсов, ручную и автоматическую добычу, передачу предметов.
- **Взаимодействует с:** `MiningSytem`, `ResourceNode`, сервисами инвентаря, системой выносливости и визуальными эффектами.
- **Паттерны:** фасад для точки добычи, наблюдатель для уведомлений об извлечении, корутины для тайминга.

### Бой и оборона
- **Отвечает за:** генерацию волн противников, управление их жизненным циклом и интеграцию с оборонительными постройками.
- **Взаимодействует с:** оркестратором врагов, сервисом волн, менеджером спавна, базовыми врагами и башнями.
- **Паттерны:** объектный пул, конечные автоматы состояний, композиция поведения через компоненты зданий.

### Игрок и взаимодействия
- **Отвечает за:** управление движением, анимациями, взаимодействием с объектами и камерой.
- **Взаимодействует с:** контроллерами игрока, интерактивными объектами, инвентарём и системой строительства.
- **Паттерны:** медиатор в оркестраторе игрока, команда для ввода, событийное взаимодействие с объектами.

### Мир и визуал
- **Отвечает за:** освещение сцены, визуальные эффекты и фоновые элементы.
- **Взаимодействует с:** контроллером спрайтов, системой дня/ночи, облаками и декоративными объектами.
- **Паттерны:** tween-анимации (DOTween), объектный пул, асинхронные корутины.

## Подробности по подсистемам
### Игровой цикл
- [`Game Director`](Scripts/_Entry%20Point/Game%20Director.cs) запускает последовательность дня, координирует основные сервисы и подписывает слушателей Zenject в [`Game Installer`](Scripts/Zenject/Game%20Installer.cs).
- [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs) инициализирует восстановление выносливости, подготовку волн врагов, генерацию контрактов и спавн поселенцев, ожидая завершения фазы дня через `DayNightSystem`.
- [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs) управляет освещением и уведомляет зарегистрированных `SpriteDimmer`, а [`DayCounter`](Scripts/Day%20night%20system/DayCounter.cs) отображает номер текущего дня.
- [`Stamina System`](Scripts/Stamina%20System/Stamina%20System.cs) хранит запас выносливости и эмитит события об изменении, а [`Stamina View`](Scripts/Stamina%20System/Stamina%20View.cs) обновляет индикатор.

### Экономика и контракты
- [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs) хранит баланс игрока и уведомляет UI (`Day Money view`, `Total Money View`) через события.
- [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs) формирует список заказов на основе предметов в `Inventory Service`, проверяет их выполнение и начисляет награду в кошелёк.
- [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs) поддерживает пул карточек, а [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs) реализует удержание для подтверждения сделки.

### Постройки
- [`BuildingService`](Scripts/Building%20system/BuildingService.cs) хранит описание построек и выдаёт точки спавна рабочих, а [`BuildingData`](Scripts/Building%20system/BuildingData.cs) и [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs) отслеживают собранные ресурсы и прогресс.
- [`BuildingOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs) взаимодействует с инвентарём, запускает визуализацию переноса через [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs) и обновляет UI с помощью [`BuildingItemView`](Scripts/Building%20system/UI/BuildingItemView.cs).
- [`Builder`](Scripts/Building%20system/Builder.cs) управляет состоянием конкретного здания, а [`BuildingView`](Scripts/Building%20system/BuildingView.cs) отображает прогресс и эффекты.
- [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs) определяет жизненный цикл построек, расширяется башнями (`ArcherTower` + [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs)), баррикадами и жилыми домами (`VillagerHome`).

### Инвентарь и крафт
- [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs) описывает общие операции со слотами (проверка вместимости, вставка, извлечение) и уведомляет подписчиков.
- [`Inventory Service`](Scripts/Inventory%20System/Inventory%20Service.cs) и [`ChestService`](Scripts/Interactable%20Object/ChestService.cs) расширяют базовый функционал, предоставляя поиски доступных ячеек и доступ из контрактов и строительства.
- [`Craft Cell Service`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs) управляет сеткой рецептов, а [`Craft Service`](Scripts/Craft%20system/Craft%20Service.cs) проверяет рецепты и тратит выносливость на создание предметов.
- [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs) обрабатывает ввод мыши, перемещая стеки между слотами, а визуальные компоненты (`CellView`, `Craft Result Cell`, `Drag Item`, `Grid Controller`, `Inventory Grid Controller`) подписываются на события сервисов.

### Поселенцы
- [`NpcService`](Scripts/NPC/NpcService.cs) хранит данные жителей и операции разблокировки, а [`NpcSystem`](Scripts/NPC/NpcSystem.cs) назначает работы, списывает ресурсы и управляет очередью найма.
- [`Npc Manager`](Scripts/NPC/Npc%20Manager.cs) отвечает за создание контроллеров и размещение персонажей у построек, используя `Villager Controller` и `Villager View` для поведения и визуализации.
- [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs) активирует панель управления, а [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs), [`ManagePanelView`](Scripts/NPC/UI/ManagePanelView.cs) и [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs) предоставляют UI назначения профессий и отображения характеристик.

### Добыча ресурсов
- [`MiningSytem`](Scripts/Mining%20System/MiningSytem.cs) регистрирует точки добычи, списывает выносливость и передаёт добытые предметы в `ChestService` или инвентарь игрока.
- [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs) запускает корутины добычи при взаимодействии игрока или поселенцев, используя [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs) для визуализации лута.
- [`IResourceNode`](Scripts/Mining%20System/IResourceNode.cs) определяет интерфейс для различных типов узлов, облегчая расширение системы.

### Бой и оборона
- [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs) подбирает волну врагов в зависимости от дня и делегирует спавн [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs).
- [`WaveService`](Scripts/Enemy%20system/WaveService.cs) конструирует состав волн на основе данных (`WaveData`, `EnemyData`), а [`EnemyWave`](Scripts/Enemy%20system/EnemyWave.cs) хранит параметры текущей атаки.
- [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs) реализует конечный автомат движения/атаки, управляет полоской здоровья через [`HealthView`](Scripts/Enemy%20system/HealthView.cs) и взаимодействует с оборонительными постройками (`ArcherTowerAttack`).

### Игрок и взаимодействия
- [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs) агрегирует движение, анимации и взаимодействия, блокируя передвижение во время активного диалога.
- [`PlayerMovement`](Scripts/Player/PlayerMovement.cs) отвечает за физику перемещения, [`PlayerView`](Scripts/Player/PlayerView.cs) — за анимации, а [`CameraFollow`](Scripts/Player/CameraFollow.cs) поддерживает слежение камеры.
- [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs) обрабатывает ввод `E/Escape`, переключая состояния интерактивов.
- [`Base Interactable Object`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs) задаёт контракт для объектов сцены; наследники (`Workbench`, `Chest`, `Stall`, `Bed`, `ManageBoard`) реализуют конкретные сценарии и взаимодействуют с инвентарём или игровым циклом.
- [`ChestGirldController`](Scripts/Interactable%20Object/ChestGirldController.cs) и [`InteractibleGlow`](Scripts/Interactable%20Object/InteractibleGlow.cs) обеспечивают вспомогательные эффекты UI/подсветки.

### Мир и визуал
- [`Sprite View Controller`](Scripts/Sprite%20View%20Controller.cs) управляет глобальными параметрами шейдеров и освещением зарегистрированных спрайтов.
- [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs) подписывается на `DayNightSystem` и регулирует интенсивность света объектов.
- [`Cloud Manager`](Scripts/Background/Cloud%20Manager.cs) и [`Cloud`](Scripts/Background/Cloud.cs) реализуют плавное движение облаков с переиспользованием объектов, а [`Tree view`](Scripts/Background/Tree%20view.cs) и [`Stive view`](Scripts/Stive%20view.cs) обновляют декоративные элементы окружения.

