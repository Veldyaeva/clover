# GridSettingsManager - Автоматическое сохранение настроек гридов

## Описание

`GridSettingsManager` - это система для автоматического сохранения и загрузки настроек DevExpress GridView. Система автоматически сохраняет:
- Ширину колонок
- Видимость колонок
- Порядок колонок
- Настройки сортировки

## Возможности

### Автоматический режим (рекомендуется)
Для `CustomGridControl` настройки сохраняются автоматически:
- При изменении ширины колонок
- При изменении видимости колонок  
- При изменении порядка колонок
- При изменении сортировки
- При закрытии формы

### Ручное управление
Можно управлять настройками вручную для любых GridView.

## Использование

### 1. Автоматический режим для CustomGridControl

```csharp
public partial class MyForm : CustomForm
{
    public MyForm(UserClass user) : base(user)
    {
        InitializeComponent();
        
        // Автоматическое сохранение включено по умолчанию
        // Ничего дополнительного делать не нужно!
    }
}
```

### 2. Отключение автоматического сохранения

```csharp
// Для конкретного CustomGridControl
customGridControl1.EnableAutoSettings = false;

// Для всех CustomGridControl на форме
this.EnableAutoGridSettings(false);
```

### 3. Пользовательские ключи настроек

```csharp
// Установка пользовательского ключа для грида
customGridControl1.SettingsKey = "MyCustomGridSettings";

// Или через расширение формы
this.SetGridSettingsKey("customGridControl1", "MyCustomGridSettings");
```

### 4. Ручное управление для обычных GridControl

```csharp
public partial class MyForm : Form
{
    private void Form_Load(object sender, EventArgs e)
    {
        // Включить автоматическое сохранение для всех GridControl
        this.EnableAutoSettingsForAllGrids(true);
        
        // Или для конкретного GridView
        GridSettingsManager.Instance.EnableAutoSettings(gridView1);
    }
    
    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Сохранить все настройки
        GridSettingsManager.Instance.SaveAllSettings();
    }
}
```

### 5. Принудительное сохранение/загрузка

```csharp
// Для всей формы
this.SaveAllGridSettings();
this.LoadAllGridSettings();

// Для конкретного CustomGridControl
customGridControl1.SaveGridSettings();
customGridControl1.LoadGridSettings();

// Для конкретного GridView
GridSettingsManager.Instance.SaveSettings(gridView1);
GridSettingsManager.Instance.LoadSettings(gridView1);
```

## Расположение файлов настроек

Настройки сохраняются в папке:
```
{ApplicationPath}/Settings/Grids/
```

Формат имени файла:
```
{FormName}_{GridName}_{ViewName}.xml
```

Пример:
```
TeamWork_customGridControl1_MainView.xml
```

## Структура XML файла настроек

```xml
<?xml version="1.0" encoding="utf-8"?>
<GridSettings SettingsKey="TeamWork_customGridControl1_MainView" SaveDate="2025-01-10 15:30:45">
  <Columns>
    <Column FieldName="AnnID" Width="100" VisibleIndex="0" Visible="true" SortOrder="None" SortIndex="-1" />
    <Column FieldName="Articul" Width="150" VisibleIndex="1" Visible="true" SortOrder="Ascending" SortIndex="0" />
    <!-- ... другие колонки ... -->
  </Columns>
</GridSettings>
```

## API Reference

### IGridSettingsManager

```csharp
public interface IGridSettingsManager
{
    void EnableAutoSettings(GridView gridView, string settingsKey = null);
    void DisableAutoSettings(GridView gridView);
    void SaveSettings(GridView gridView, string settingsKey = null);
    void LoadSettings(GridView gridView, string settingsKey = null);
    void SaveAllSettings();
    void LoadAllSettings();
}
```

### FormGridSettingsExtensions

```csharp
public static class FormGridSettingsExtensions
{
    public static void EnableAutoGridSettings(this Form form, bool enableAutoSettings = true);
    public static void SaveAllGridSettings(this Form form);
    public static void LoadAllGridSettings(this Form form);
    public static void EnableAutoSettingsForAllGrids(this Form form, bool enableAutoSettings = true);
    public static void SetGridSettingsKey(this Form form, string gridControlName, string settingsKey);
}
```

### CustomGridControl свойства

```csharp
public class CustomGridControl : GridControl
{
    // Включает/отключает автоматическое сохранение (по умолчанию true)
    public bool EnableAutoSettings { get; set; } = true;
    
    // Пользовательский ключ настроек (опционально)
    public string SettingsKey { get; set; }
    
    // Методы
    public void SaveGridSettings();
    public void LoadGridSettings();
}
```

## Миграция с существующего подхода

### Было (старый подход):
```csharp
private void LoadGridSettings()
{
    _gridHelper.LoadGridViewSettings(gridView1, "gridView1Layout.xml");
    _gridHelper.LoadGridViewSettings(gridView2, "gridView2Layout.xml");
    // ... много строк для каждого грида
}

private void SaveGridSettings()
{
    _gridHelper.SaveGridViewSettings(gridView1, "gridView1Layout.xml");
    _gridHelper.SaveGridViewSettings(gridView2, "gridView2Layout.xml");
    // ... много строк для каждого грида
}
```

### Стало (новый подход):
```csharp
private void LoadGridSettings()
{
    // Включаем автоматическое сохранение для всех CustomGridControl
    this.EnableAutoGridSettings(true);
    
    // Только для обычных GridControl (если есть)
    _gridHelper.LoadGridViewSettings(standardGridView, "standardGridLayout.xml");
}

private void SaveGridSettings()
{
    // Автоматически сохраняем все CustomGridControl
    this.SaveAllGridSettings();
    
    // Только для обычных GridControl (если есть)
    _gridHelper.SaveGridViewSettings(standardGridView, "standardGridLayout.xml");
}
```

## Преимущества новой системы

1. **Автоматизация** - настройки сохраняются автоматически без дополнительного кода
2. **Централизация** - все настройки управляются из одного места
3. **Безопасность** - система устойчива к ошибкам и не прерывает работу приложения
4. **Гибкость** - можно настроить как автоматический, так и ручной режим
5. **Совместимость** - работает параллельно с существующей системой
6. **Производительность** - сохранение происходит с задержкой, избегая частых операций записи

## Рекомендации

1. Используйте `CustomGridControl` вместо обычного `GridControl` для новых форм
2. Оставьте `EnableAutoSettings = true` (по умолчанию) для автоматического режима
3. Задавайте пользовательские ключи только при необходимости (например, для одинаковых гридов на разных вкладках)
4. Для существующих форм добавьте вызов `this.EnableAutoGridSettings(true)` в метод загрузки настроек
5. Система совместима со старым подходом - можно мигрировать постепенно