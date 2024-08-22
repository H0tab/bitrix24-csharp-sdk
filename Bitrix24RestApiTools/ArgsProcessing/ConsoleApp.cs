using System.Diagnostics.CodeAnalysis;
using Bitrix24RestApiClient.Api;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Bitrix24RestApiTools.Logic;
using Bitrix24RestApiTools.Models;
using Humanizer;
using PowerArgs;

namespace Bitrix24RestApiTools.ArgsProcessing;

public class ConsoleApp
{
    [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
    public bool Help { get; set; }

    [ArgActionMethod, ArgShortcut("-g"), ArgDescription("Generate model by fields info")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public async Task GenerateModelByFields(GenerateModelArgs args)
    {
        var client = new Bitrix24Client(args.WebhookUrl, new ConsoleLogger<Bitrix24Client>());
        object body = args.EntityTypeId == null
            ? new { }
            : new { entityTypeId = args.EntityTypeId };

        var response = await client.SendPostRequest<object, ExtFieldsResponse>(new EntryPointPrefix(args.FieldsEntryPoint), EntityMethod.None, body);
        var fields = response.Fields.Result;
            
        var outputPath = args.OutputPath;
        if(!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        var outputModelsPath = Path.Combine(args.OutputPath, "Models");
        if (!Directory.Exists(outputModelsPath))
            Directory.CreateDirectory(outputModelsPath);

        var classCodeGenerator = new ClassCodeGenerator(args.ClassName, args.ClassDescription, fields);

        var modelClassCode = classCodeGenerator.GenerateModelClass();
        await File.WriteAllTextAsync(Path.Combine(outputModelsPath, $"{args.ClassName}.cs"), modelClassCode);

        var fieldNamesClassCode = classCodeGenerator.GenerateFieldNamesClass();
        await File.WriteAllTextAsync(Path.Combine(outputModelsPath, $"{args.ClassName}Fields.cs"), fieldNamesClassCode);

        var containerClassCode = classCodeGenerator.GenerateContainerClass();
        await File.WriteAllTextAsync(Path.Combine(outputPath, $"{args.ClassName}s.cs"), containerClassCode);
    }

    [ArgActionMethod, ArgShortcut("-gall"), ArgDescription("Generate all models by fields info")]
    public async Task GenerateAllModelsByFields(GenerateAllModelsArgs args)
    {
        var models = new List<ClassGeneratorArgs>
        { 
            new() {description= "Cделки", className= "Deal", entryPoint= "crm.deal.fields", dirs= new List<string> { "Crm", "Deal" }, responseClass= "FieldsResponse" },
            new() {description= "Контакты", className= "Contact", entryPoint= "crm.contact.fields", dirs= new List<string> { "Crm", "Contact" }, responseClass= "FieldsResponse" },
            new() {description= "Лиды", className= "Lead", entryPoint= "crm.lead.fields", dirs= new List<string> { "Crm", "Lead" }, responseClass= "FieldsResponse" },
            new() {description= "Компании", className= "Company", entryPoint= "crm.company.fields", dirs= new List<string> { "Crm", "Company" }, responseClass= "FieldsResponse" },
            new() {description= "Контакыт компаний", className= "CompanyContact", entryPoint= "crm.company.contact.fields", dirs= new List<string> { "Crm", "Company", "Contact" }, responseClass= "FieldsResponse" },
            new() {description= "Компании контактов", className= "ContactCompany", entryPoint= "crm.contact.company.fields", dirs= new List<string> { "Crm", "Contact", "Company" }, responseClass= "FieldsResponse" },
            new() {description= "Направления сделок, устаревшее", className= "DealCategory", entryPoint= "crm.dealcategory.fields", dirs= new List<string> { "Crm", "DealCategory" }, responseClass= "FieldsResponse" },
            new() {description= "Статусы счетов", className= "InvoiceStatus", entryPoint= "crm.invoice.status.fields", dirs= new List<string> { "Crm", "Invoice", "Status" }, responseClass= "FieldsResponse" },
            new() {description= "Коммерческие предложения", className= "Quote", entryPoint= "crm.quote.fields", dirs= new List<string> { "Crm", "Quote" }, responseClass= "FieldsResponse" },
            new() {description= "Счета (старые}", className= "Invoice", entryPoint= "crm.invoice.fields", dirs= new List<string> { "Crm", "Invoice" }, responseClass= "FieldsResponse" },
            new() {description= "Регулярные счета", className= "InvoiceRecurring", entryPoint= "crm.invoice.recurring.fields", dirs= new List<string> { "Crm", "Invoice", "Recurring" }, responseClass= "FieldsResponse" },
            new() {description= "Способы оплат", className= "PaySystem", entryPoint= "crm.paysystem.fields", dirs= new List<string> { "Crm", "PaySystem" }, responseClass= "FieldsResponse" },
            new() {description= "Типы плательщиков", className= "PersonType", entryPoint= "crm.persontype.fields", dirs= new List<string> { "Crm", "PersonType" }, responseClass= "FieldsResponse" },
            new() {description= "Привязки элементов crm в таймлайне", className= "TimelineBindings", entryPoint= "crm.timeline.bindings.fields", dirs= new List<string> { "Crm", "Timeline", "Bindings" }, responseClass= "FieldsResponse" },
            new() {description= "Комментарии таймлайна", className= "TimelineComment", entryPoint= "crm.timeline.comment.fields", dirs= new List<string> { "Crm", "Timeline", "Comment" }, responseClass= "FieldsResponse" },
            new() {description= "Дела. Дела описывают выполненную, текущую и запланированную работу по лидам, контактам, компаниям и сделкам. Делятся на звонки, встречи и e-mail сообщения.", className= "Activity", entryPoint= "crm.activity.fields", dirs= new List<string> { "Crm", "Activity" }, responseClass= "FieldsResponse" },
            new() {description= "Коммуникации для активности. Коммуникации хранят номера телефонов в звонках, email-адреса в письмах, имена во встречах.", className= "ActivityCommunication", entryPoint= "crm.activity.communication.fields", dirs= new List<string> { "Crm", "Activity", "Communication" }, responseClass= "FieldsResponse" },
            new() {description= "Адреса реквизитов", className= "Address", entryPoint= "crm.address.fields", dirs= new List<string> { "Crm", "Address" }, responseClass= "FieldsResponse" },
            new() {description= "Реквизиты", className= "Requisite", entryPoint= "crm.requisite.fields", dirs= new List<string> { "Crm", "Requisite" }, responseClass= "FieldsResponse" },
            new() {description= "Банковские реквизиты", className= "RequisiteBankdetail", entryPoint= "crm.requisite.bankdetail.fields", dirs= new List<string> { "Crm", "Requisite", "Bankdetail" }, responseClass= "FieldsResponse" },
            new() {description= "Связи реквизитов. Связи определяют, какие реквизиты выбраны для сделки, предложения или счёта. При этом реквизиты должны принадлежать выбранной компании или контакту. Так, если в счёте в качестве покупателя выбрана компания, то реквизиты покупателя должны принадлежать этой компании. В качестве продавца может быть выбрана только компания из справочника \"Реквизиты ваших компаний\".", className= "RequisiteLink", entryPoint= "crm.requisite.link.fields", dirs= new List<string> { "Crm", "Requisite", "Link" }, responseClass= "FieldsResponse" },
            new() {description= "Шаблоны реквизитов", className= "RequisitePreset", entryPoint= "crm.requisite.preset.fields", dirs= new List<string> { "Crm", "Requisite", "Preset" }, responseClass= "FieldsResponse" },
            new() {description= "Список полей из набора полей шаблона для определенного реквизита.", className= "RequisitePresetField", entryPoint= "crm.requisite.preset.field.fields", dirs= new List<string> { "Crm", "Requisite", "Preset", "Field" }, responseClass= "FieldsResponse" },
            new() {description= "Пользовательские поля", className= "Userfield", entryPoint= "crm.userfield.fields", dirs= new List<string> { "Crm", "Userfield" }, responseClass= "FieldsResponse" },
            new() {description= "Каталог товаров", className= "Catalog", entryPoint= "crm.catalog.fields", dirs= new List<string> { "Crm", "Catalog" }, responseClass= "FieldsResponse" },
            new() {description= "Валюты", className= "Currency", entryPoint= "crm.currency.fields", dirs= new List<string> { "Crm", "Currency" }, responseClass= "FieldsResponse" },
            new() {description= "Локализаций для валют", className= "CurrencyLocalizations", entryPoint= "crm.currency.localizations.fields", dirs= new List<string> { "Crm", "Currency", "Localizations" }, responseClass= "FieldsResponse" },
            new() {description= "Единицы измерения", className= "Measure", entryPoint= "crm.measure.fields", dirs= new List<string> { "Crm", "Measure" }, responseClass= "FieldsResponse" },
            new() {description= "Разделы товаров", className= "ProductSection", entryPoint= "crm.productsection.fields", dirs= new List<string> { "Crm", "ProductSection" }, responseClass= "FieldsResponse" },
            new() {description= "Товарные позиции (старые}", className= "ProductRowOld", entryPoint= "crm.productrow.fields", dirs= new List<string> { "Crm", "ProductRowOld" }, responseClass= "FieldsResponse" },
            new() {description= "Товарные позиции", className= "ProductRow", entryPoint= "crm.item.productrow.fields", dirs= new List<string> { "Crm", "ProductRow" }, responseClass= "ExtFieldsResponse" },
            new() {description= "Товары", className= "Product", entryPoint= "crm.product.fields", dirs= new List<string> { "Crm", "Product" }, responseClass= "FieldsResponse" },
            new() {description= "Свойства товар", className= "ProductProperty", entryPoint= "crm.product.property.fields", dirs= new List<string> { "Crm", "Product", "Property" }, responseClass= "FieldsResponse" },
            new() {description= "Описание полей дополнительных настроек свойства товаров пользовательского типа", className= "ProductPropertySettings", entryPoint= "crm.product.property.settings.fields", dirs= new List<string> { "Crm", "Product", "Property", "Settings" }, responseClass= "ExtFieldsResponse" },
            new() {description= "Множественные поля", className= "Multifield", entryPoint= "crm.multifield.fields", dirs= new List<string> { "Crm", "Multifield" }, responseClass= "FieldsResponse" },
            new() {description= "Перечисления", className= "Enum", entryPoint= "crm.enum.fields", dirs= new List<string> { "Crm", "Enum" }, responseClass= "FieldsResponse" },
            new() {description= "Справочники", className= "Status", entryPoint= "crm.status.fields", dirs= new List<string> { "Crm", "Status" }, responseClass= "FieldsResponse" },
            new() {description= "Ставки НДС", className= "Vat", entryPoint= "crm.vat.fields", dirs= new List<string> { "Crm", "Vat" }, responseClass= "FieldsResponse" },
            new() {description= "Типы смарт-процессов", className= "SmartProcessType", entryPoint= "crm.type.fields", dirs= new List<string> { "Crm", "SmartProcessType" }, responseClass= "ExtFieldsResponse"},
            new() {description= "Пользователи", className= "User", entryPoint= "user.fields", dirs= new List<string> { "User" }, responseClass= "UserFieldsResponse"}
        };

        //Направления требуют указания entityTypeId
        //(description: "Направления", className: "Category", entryPoint: "crm.category.fields", dirs: new List<string> { "Crm", "Category", "Models" }),
        //EntityTypeIdEnum
        var client = new Bitrix24Client(args.WebhookUrl, new ConsoleLogger<Bitrix24Client>());
        var bitrix24 = new Bitrix24(client);
        var smartProcesses = await bitrix24.Crm.SmartProcessTypes.List(x=>x.SelectAll());
        models.AddRange(smartProcesses.Result.Items
            .Select(smartProcess => new
            {
                smartProcess, className = smartProcess.Title.Transform(To.LowerCase, To.TitleCase).Dehumanize()
            })
            .Select(t => new ClassGeneratorArgs
            {
                description = t.smartProcess.Title ?? string.Empty,
                className = t.className,
                entryPoint = "crm.item.fields",
                dirs = ["SmartProcesses", t.className],
                responseClass = "ExtFieldsResponse",
                entityTypeId = t.smartProcess.EntityTypeId
            }));

        foreach (var model in models)
        {
            await GenerateModelByFields(new GenerateModelArgs
            {
                EntityTypeId = model.entityTypeId,
                ClassDescription = model.description,
                ClassName = $"{args.ClassPrefix}{model.className}",
                FieldsEntryPoint = model.entryPoint,
                OutputPath = Path.Combine(args.OutputPath, string.Join("\\", model.dirs)),
                WebhookUrl = args.WebhookUrl
            });
        }
    }
}