﻿using Newtonsoft.Json;
using Bitrix24RestApiClient.Core.Attributes;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Models.CrmTypes;
using Bitrix24RestApiClient.Core.Models.CrmTypes.CrmFile;

namespace Bitrix24RestApiClient.Api.Crm.CrmProduct.Models;

public class Product: IAbstractEntity
{
    /// <summary>
    /// Идентификатор			
    /// Тип: integer	
    /// Только для чтения
    /// </summary>
    [JsonProperty(AbstractEntityFields.Id)]
    public int? Id { get; set; }

    /// <summary>
    /// Активен
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(ProductFields.Active, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool Active
    {
        get
        {
            return ActiveExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            ActiveExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// Активен 			
    /// Тип: char
    /// </summary>
    [JsonProperty(ProductFields.Active)]
    public string ActiveExt { get; set; }

    /// <summary>
    /// Идентификатор каталога  			
    /// Тип: integer 
    /// Только для чтения
    /// </summary>
    [JsonProperty(ProductFields.CatalogId)]
    public int? CatalogId { get; set; }

    /// <summary>
    /// Кем создан товар    			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.CreatedBy)]
    public int? CreatedBy { get; set; }

    /// <summary>
    /// Идентификатор валюты    			
    /// Тип: string
    /// </summary>
    [JsonProperty(ProductFields.CurrencyId)]
    public string CurrencyId { get; set; }

    /// <summary>
    /// Дата создания товара 			
    /// Тип: datetime
    /// </summary>
    [JsonProperty(ProductFields.DateCreate)]
    public DateTimeOffset? DateCreate { get; set; }

    /// <summary>
    /// Описание    			
    /// Тип: string
    /// </summary>
    [JsonProperty(ProductFields.Description)]
    public string Description { get; set; }

    /// <summary>
    /// Тип описания    			
    /// Тип: string
    /// </summary>
    [JsonProperty(ProductFields.DescriptionType)]
    public string DescriptionType { get; set; }

    /// <summary>
    /// Детальная картинка  			
    /// Тип: product_file
    /// </summary>
    [JsonProperty(ProductFields.DetailPicture)]
    public CrmFile DetailPicture { get; set; }

    /// <summary>
    /// Единица измерения 			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.Measure)]
    public int? Measure { get; set; }

    /// <summary>
    /// Кем изменён товар   			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.ModifiedBy)]
    public int? ModifiedBy { get; set; }

    /// <summary>
    /// Название 			
    /// Тип: string 
    /// Обязательное
    /// </summary>
    [JsonProperty(ProductFields.Name)]
    public string Name { get; set; }

    /// <summary>
    /// Картинка для анонса 			
    /// Тип: product_file
    /// </summary>
    [JsonProperty(ProductFields.PreviewPicture)]
    public CrmFile PreviewPicture { get; set; }

    /// <summary>
    /// Цена 			
    /// Тип: double
    /// </summary>
    [JsonProperty(ProductFields.Price)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Идентификатор раздела   			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.SectionId)]
    public int? SectionId { get; set; }

    /// <summary>
    /// Сортировка 			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.Sort)]
    public int? Sort { get; set; }

    /// <summary>
    /// Дата изменения товара   			
    /// Тип: datetime 
    /// Неизменяемое поле
    /// </summary>
    [JsonProperty(ProductFields.TimestampX)]
    public DateTimeOffset? TimestampX { get; set; }

    /// <summary>
    /// Идентификатор ставки НДС 			
    /// Тип: integer
    /// </summary>
    [JsonProperty(ProductFields.VatId)]
    public int? VatId { get; set; }

    /// <summary>
    /// НДС включён в цену
    /// Тип: char	
    /// </summary>
    [JsonIgnore]
    [CrmField(ProductFields.VatIncluded, CrmFieldSubTypeEnum.Char_YesNo)]
    public bool VatIncluded
    {
        get
        {
            return VatIncludedExt == YesNoEnum.Y.ToString("F");
        }
        set
        {
            VatIncludedExt = value
                ? YesNoEnum.Y.ToString("F")
                : YesNoEnum.N.ToString("F");
        }
    }

    /// <summary>
    /// НДС включён в цену 			
    /// Тип: char
    /// </summary>
    [JsonProperty(ProductFields.VatIncluded)]
    public string VatIncludedExt { get; set; }

    /// <summary>
    /// Внешний код 			
    /// Тип: string
    /// </summary>
    [JsonProperty(ProductFields.XmlId)]
    public string XmlId { get; set; }
}