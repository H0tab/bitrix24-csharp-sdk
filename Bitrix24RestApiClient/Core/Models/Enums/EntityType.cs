namespace Bitrix24RestApiClient.Core.Models.Enums
{
    public class EntryPointPrefix
    {
        public EntryPointPrefix() { }
        public EntryPointPrefix(string value) {
            Value = value;
        }
        public string Value { get; private set; }
        public static EntryPointPrefix StageHistory = new() { Value = "crm.stagehistory" };        
        public static EntryPointPrefix DealProductRows = new() { Value = "crm.deal.productrows" };        
        public static EntryPointPrefix Batch = new() { Value = "batch" };
        public static EntryPointPrefix Status = new() { Value = "crm.status" };
        public static EntryPointPrefix SmartProcessType = new() { Value = "crm.type" };
        public static EntryPointPrefix PaySystem = new() { Value = "crm.paysystem" };
        public static EntryPointPrefix Invoice = new() { Value = "crm.invoice" };
        public static EntryPointPrefix Product = new() { Value = "crm.product" };
        public static EntryPointPrefix ProductSection = new() { Value = "crm.productsection" };
        public static EntryPointPrefix Company = new() { Value = "crm.company" };
        public static EntryPointPrefix Deal = new() { Value = "crm.deal" };
        public static EntryPointPrefix DealContact = new() { Value = "crm.deal.contact" };
        public static EntryPointPrefix CompanyContact = new() { Value = "crm.company.contact" };
        public static EntryPointPrefix DealContactItems = new() { Value = "crm.deal.contact.items" };
        public static EntryPointPrefix CompanyContactItems = new() { Value = "crm.company.contact.items" };
        public static EntryPointPrefix Lead = new() { Value = "crm.lead" };
        public static EntryPointPrefix DealUserFields = new() { Value = "crm.deal.userfield" };
        public static EntryPointPrefix Contact = new() { Value = "crm.contact" };
        public static EntryPointPrefix TimelineComment = new() { Value = "crm.timeline.comment" };
        public static EntryPointPrefix Activity = new() { Value = "crm.activity" };
        public static EntryPointPrefix User = new() { Value = "user" };
        public static EntryPointPrefix Item = new() { Value = "crm.item" };
        public static EntryPointPrefix CrmProductRow = new() { Value = "crm.item.productrow" };
        public static EntryPointPrefix RequisiteLink = new() { Value = "crm.requisite.link" };
        public static EntryPointPrefix RequisiteBankdetail = new() { Value = "crm.requisite.bankdetail" };
        public static EntryPointPrefix RequisitePresetField = new() { Value = "crm.requisite.preset.field" };
        public static EntryPointPrefix RequisitePreset = new() { Value = "crm.requisite.preset" };
        public static EntryPointPrefix Requisite = new() { Value = "crm.requisite" };
        public static EntryPointPrefix Task = new() { Value = "tasks.task" };
    }
}
