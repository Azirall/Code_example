# Обзор игровых скриптов

Документ группирует скрипты по основным подсистемам проекта и поясняет, как они взаимодействуют между собой.

## Подсистемы и ключевые классы

### Игровой цикл

**Ключевые скрипты**
- [`Game Director`](Scripts/_Entry%20Point/Game%20Director.cs)
- [`GameCycleOrchestrator`](Scripts/_Entry%20Point/GameCycleOrchestrator.cs)
- [`DayNightSystem`](Scripts/Day%20night%20system/DayNightSystem.cs)
- [`DayCounter`](Scripts/Day%20night%20system/DayCounter.cs)
- [`Stamina System`](Scripts/Stamina%20System/Stamina%20System.cs)
- [`Stamina View`](Scripts/Stamina%20System/Stamina%20View.cs)

**Обязанности**
- Управляет сменой дня и ночи, обновлением интерфейса и восстановлением ресурсов.
- Рассылает события другим подсистемам при смене фазы игры.

**Взаимодействия**
- Связывается с системами выносливости, контрактов, поселенцев и обороны.
- Сообщает об изменении времени суток визуальным контроллерам (`SpriteDimmer`).

**Паттерны**
- Оркестратор в лице `GameCycleOrchestrator`.
- Подписка на события Unity и Zenject-инъекции зависимостей.

**Подробности**
- `Game Director` запускает дневной цикл и регистрирует сервисы через [`Game Installer`](Scripts/Zenject/Game%20Installer.cs).
- `DayNightSystem` контролирует освещение и уведомляет зарегистрированные компоненты.
- `Stamina System` оповещает UI представления о текущем уровне выносливости.

---

### Экономика и контракты

**Ключевые скрипты**
- [`Wallet`](Scripts/Money%20and%20stats/Wallet.cs)
- [`Day Money view`](Scripts/Money%20and%20stats/Day%20Money%20view.cs)
- [`ContractSystem`](Scripts/Contract%20system/ContractSystem.cs)
- [`ContractPanelController`](Scripts/Contract%20system/ContractPanelController.cs)
- [`ContractView`](Scripts/Contract%20system/UI/ContractView.cs)

**Обязанности**
- Формирует и проверяет заказы на поставку ресурсов.
- Ведёт учёт денег игрока и обновляет UI.

**Взаимодействия**
- Использует `Inventory Service` для проверки предметов.
- Передаёт награды в `Wallet` и обновляет связанные представления.

**Паттерны**
- Сервис-ориентированный подход (`ContractSystem`, `Wallet`).
- Наблюдатель для обновления интерфейсов.
- Пул UI-элементов в `ContractPanelController`.

**Подробности**
- `ContractSystem` проверяет выполнение условий контракта и списывает ресурсы.
- `ContractView` реализует задержку подтверждения через удержание кнопки.

---

### Постройки

**Ключевые скрипты**
- [`BuildingService`](Scripts/Building%20system/BuildingService.cs)
- [`BuildingOrchestrator`](Scripts/Building%20system/BuildingOrchestrator.cs)
- [`Builder`](Scripts/Building%20system/Builder.cs)
- [`BuildingData`](Scripts/Building%20system/BuildingData.cs)
- [`BuildingProgress`](Scripts/Building%20system/BuildingProgress.cs)
- [`BuildingView`](Scripts/Building%20system/BuildingView.cs)
- [`BuildingItemView`](Scripts/Building%20system/UI/BuildingItemView.cs)
- [`BaseBuilding`](Scripts/Building%20system/BaseBuilding.cs)
- [`ArcherTowerAttack`](Scripts/Building%20system/ArcherTowerAttack.cs)
- [`ResourceTransferController`](Scripts/Player/Resours%20transfer%20anim/ResourceTransferController.cs)
- Производные постройки: [`ArcherTower`](Scripts/Building%20system/Buildings/ArcherTower.cs), [`Barricade`](Scripts/Building%20system/Buildings/Barricade.cs), [`VillagerHome`](Scripts/Building%20system/Buildings/VillagerHome.cs), [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs)

**Обязанности**
- Описывает данные построек, их прогресс и визуализацию.
- Обеспечивает доставку ресурсов к стройкам и взаимодействие с жителями.

**Взаимодействия**
- Получает ресурсы из инвентаря и системы переносов (`ResourceTransferController`).
- Передаёт данные в UI и защитные механики (например, `ArcherTowerAttack`).

**Паттерны**
- Сервисы данных и оркестратор для координации процессов.
- Событийная модель для обновления UI.
- Объектный пул для анимаций перемещения ресурсов.

**Подробности**
- `BuildingService` хранит конфигурации и выдаёт точки спавна рабочих.
- `BuildingOrchestrator` распределяет ресурсы и синхронизирует прогресс со сценой.
- `BaseBuilding` задаёт общие стадии строительства для наследников.

---

### Инвентарь и крафт

**Ключевые скрипты**
- [`ServiceBase`](Scripts/Drag%20and%20drop/ServiceBase.cs)
- [`Inventory Service`](Scripts/Inventory%20System/Inventory%20Service.cs)
- [`ChestService`](Scripts/Interactable%20Object/ChestService.cs)
- [`Craft Cell Service`](Scripts/Drag%20and%20drop/Craft%20Cell%20Service.cs)
- [`Craft Service`](Scripts/Craft%20system/Craft%20Service.cs)
- [`DragDropOrchestrator`](Scripts/Drag%20and%20drop/_Drag%20Drop%20Orchestrator.cs)
- UI-компоненты: [`CellView`](Scripts/Drag%20and%20drop/CellView.cs), [`Craft Result Cell`](Scripts/Drag%20and%20drop/Craft%20Result%20Cell.cs), [`Drag Item`](Scripts/Drag%20and%20drop/Drag%20Item.cs), [`Grid Controller`](Scripts/Drag%20and%20drop/Grid%20Controller.cs), [`Inventory Grid Controller`](Scripts/Inventory%20System/Inventory%20Grid%20Controller.cs)

**Обязанности**
- Унифицирует операции со слотами и применение рецептов.
- Управляет перемещением предметов и обновлением UI.

**Взаимодействия**
- Интегрируется с системами контрактов, строительства и сундуков.
- Поддерживает ввод мышью через `DragDropOrchestrator`.

**Паттерны**
- Шаблонный метод в `ServiceBase`.
- Наблюдатель для синхронизации UI с состоянием слотов.
- Стратегия выбора количества предметов при перемещении.

**Подробности**
- `Inventory Service` и `ChestService` расширяют базовый функционал слот-сервисов.
- `Craft Service` проверяет рецепты и списывает выносливость.
- `DragDropOrchestrator` обрабатывает события мыши и передаёт их компонентам вида.

---

### Поселенцы

**Ключевые скрипты**
- [`NpcSystem`](Scripts/NPC/NpcSystem.cs)
- [`NpcService`](Scripts/NPC/NpcService.cs)
- [`Npc Manager`](Scripts/NPC/Npc%20Manager.cs)
- [`VillagerData`](Scripts/NPC/VillagerData.cs)
- [`Villager Controller`](Scripts/NPC/Villager%20Controller.cs)
- [`Villager View`](Scripts/NPC/Villager%20View.cs)
- UI-компоненты: [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs), [`ManagePanelController`](Scripts/NPC/UI/ManagePanelController.cs), [`ManagePanelView`](Scripts/NPC/UI/ManagePanelView.cs), [`VillagerInfoView`](Scripts/NPC/UI/VillagerInfoView.cs)

**Обязанности**
- Создаёт и управляет жителями, назначает им профессии и задачи.
- Обновляет интерфейс управления поселенцами.

**Взаимодействия**
- Работает с постройками, инвентарём и системой экономики.
- Управляет наймом и размещением через `Npc Manager` и `ManageBoard`.

**Паттерны**
- Внедрение зависимостей (Zenject).
- Событийные каналы между сервисами и UI.
- Объектный пул контроллеров.

**Подробности**
- `NpcSystem` назначает работы и списывает ресурсы.
- `Npc Manager` создаёт контроллеры персонажей и передаёт им данные.
- `ManagePanelController` синхронизирует UI-формы назначения профессий.

---

### Добыча ресурсов

**Ключевые скрипты**
- [`MiningSytem`](Scripts/Mining%20System/MiningSytem.cs)
- [`ResourceNode`](Scripts/Mining%20System/ResourceNode.cs)
- [`IResourceNode`](Scripts/Mining%20System/IResourceNode.cs)
- [`ExtractedItemView`](Scripts/Mining%20System/View/ExtractedItemView.cs)

**Обязанности**
- Регистрирует источники ресурсов и управляет процессом добычи.
- Передаёт добытые предметы в инвентарь игрока или сундуки.

**Взаимодействия**
- Обменивается данными с системами выносливости и инвентаря.
- Вызывает визуальные эффекты извлечения ресурсов.

**Паттерны**
- Фасад для взаимодействия с узлами ресурса.
- Наблюдатель для уведомлений об успешной добыче.
- Коррутины для тайминга процессов.

**Подробности**
- `MiningSytem` списывает выносливость и триггерит выдачу предметов.
- `ResourceNode` запускает корутины добычи и использует `ExtractedItemView` для отображения лута.

---

### Бой и оборона

**Ключевые скрипты**
- [`EnemyOrchestrator`](Scripts/Enemy%20system/EnemyOrchestrator.cs)
- [`WaveService`](Scripts/Enemy%20system/WaveService.cs)
- [`EnemyManager`](Scripts/Enemy%20system/EnemyManager.cs)
- [`EnemyWave`](Scripts/Enemy%20system/EnemyWave.cs)
- [`BaseEnemy`](Scripts/Enemy%20system/BaseEnemy.cs)
- [`HealthView`](Scripts/Enemy%20system/HealthView.cs)
- Производные враги: [`Skeleton`](Scripts/Enemy%20system/Skeleton.cs), [`Zombie`](Scripts/Enemy%20system/Zombie.cs)

**Обязанности**
- Генерирует волны противников и управляет их жизненным циклом.
- Обновляет UI здоровья и взаимодействует с оборонительными постройками.

**Взаимодействия**
- Согласует расписание атак с `GameCycleOrchestrator` и башнями (`ArcherTowerAttack`).
- Передаёт урон и события смерти в экономику и систему поселенцев.

**Паттерны**
- Объектный пул для спавна врагов.
- Конечные автоматы состояний внутри `BaseEnemy`.
- Композиция поведения через компоненты построек и врагов.

**Подробности**
- `WaveService` подбирает состав волн на основе данных (`WaveData`, `EnemyData`).
- `EnemyManager` отвечает за фактический спавн и уборку сущностей.
- `HealthView` отображает текущее здоровье врага.

---

### Игрок и взаимодействия

**Ключевые скрипты**
- [`PlayerOrchestrator`](Scripts/Player/PlayerOrchestrator.cs)
- [`PlayerMovement`](Scripts/Player/PlayerMovement.cs)
- [`PlayerView`](Scripts/Player/PlayerView.cs)
- [`PlayerInteraction`](Scripts/Player/PlayerInteraction.cs)
- [`CameraFollow`](Scripts/Player/CameraFollow.cs)
- [`Base Interactable Object`](Scripts/Interactable%20Object/Base%20Interactable%20Object.cs)
- Наследники интерактивов: [`Workbench`](Scripts/Interactable%20Object/Workbench.cs), [`Chest`](Scripts/Interactable%20Object/Chest.cs), [`Stall`](Scripts/Interactable%20Object/Stall.cs), [`Bed`](Scripts/Interactable%20Object/Bed.cs), [`ManageBoard`](Scripts/Building%20system/Buildings/ManageBoard.cs)
- Вспомогательные компоненты: [`ChestGirldController`](Scripts/Interactable%20Object/ChestGirldController.cs), [`InteractibleGlow`](Scripts/Interactable%20Object/InteractibleGlow.cs)

**Обязанности**
- Управляет движением, анимациями и взаимодействиями игрока с миром.
- Поддерживает работу камеры и подсветку активных объектов.

**Взаимодействия**
- Использует интерактивные объекты для доступа к крафту, сундукам и постройкам.
- Сигнализирует другим подсистемам о начале и завершении взаимодействий.

**Паттерны**
- Медиатор (`PlayerOrchestrator`) для агрегации поведения.
- Команда для обработки ввода.
- Событийные уведомления для объектов сцены.

**Подробности**
- `PlayerMovement` отвечает за физику, `PlayerView` — за анимации.
- `PlayerInteraction` переключает состояния интерактивов и UI.
- `CameraFollow` отслеживает игрока и поддерживает плавное движение камеры.

---

### Мир и визуал

**Ключевые скрипты**
- [`Sprite View Controller`](Scripts/Sprite%20View%20Controller.cs)
- [`SpriteDimmer`](Scripts/Test%20sprite%20visual/SpriteDimmer.cs)
- [`Cloud Manager`](Scripts/Background/Cloud%20Manager.cs)
- [`Cloud`](Scripts/Background/Cloud.cs)
- [`Tree view`](Scripts/Background/Tree%20view.cs)
- [`Stive view`](Scripts/Stive%20view.cs)

**Обязанности**
- Отвечает за освещение, атмосферные эффекты и декоративные элементы.
- Управляет изменениями визуальных параметров в зависимости от времени суток.

**Взаимодействия**
- Получает уведомления от `DayNightSystem` и обновляет зарегистрированные спрайты.
- Сотрудничает с игроком и окружением для отображения визуальных эффектов.

**Паттерны**
- Tween-анимации (DOTween) для плавных переходов.
- Объектный пул для повторного использования визуальных объектов.
- Асинхронные корутины для управления таймингом эффектов.

**Подробности**
- `Sprite View Controller` управляет глобальными параметрами материалов.
- `Cloud Manager` и `Cloud` отвечают за движение облаков.
- `Tree view` и `Stive view` обновляют декоративные объекты сцены.

