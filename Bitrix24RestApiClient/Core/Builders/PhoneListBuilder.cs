﻿using System.Collections.Generic;
using Bitrix24RestApiClient.Core.Models.Enums;
using Bitrix24RestApiClient.Core.Builders.Interfaces;
using Bitrix24RestApiClient.Core.Models.CrmMultiField;
using Bitrix24RestApiClient.Core.Models.CrmMultiField.implementations;

namespace Bitrix24RestApiClient.Core.Builders;

public class PhoneListBuilder: IPhoneListBuilder
{
    private readonly List<CrmMultiField> fields = new();

    public IPhoneListBuilder SetField(string phone, string type = EmailType.Рабочий)
    {
        fields.Add(new CrmMultiFieldPhone(phone, type));
        return this;
    }

    public List<CrmMultiField> Build() => fields;
}