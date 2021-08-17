namespace OrganizationPanel
{
    public static class FormMessages
    {
        public static string Error => "Ошибка.";
        public static string NoEmployees => "Сотрудники отсутствуют для организации.";
        public static string EmployeesNotInserted => "Не удалось добавить сотрудников в базу данных из CSV для организации.";
        public static string NoOrganizations => "В базе данных не найдено организаций.";
        public static string NoSelectedOrg => "Не выбрана организация.";
        public static string CantExport => "Невозможно выгрузить сотрудников.";
        public static string CantInsertRecords => "Записи в базу данных не добавлены.";
        public static string HaventRecords => "Записи в базе отсутствуют.";
        public static string CsvHaventNewEmployees => "Файл CSV не содержит новых сотрудников для выбранной организации.";
    }
}