﻿using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Services
{
    public class GeneratorService : IGeneratorService
    {
        public string GenerateKey(int length, KeyTypes keyTypes)
        {
            var random = new Random();
            var tempPassword = new List<char>();
            var variantlist = GetVariantTypes(keyTypes);
            for (var index = 0; index < length; index++)
            {
                var variantIndex = random.Next(variantlist.Count());
                var variantName = variantlist.ElementAt(variantIndex);
                var value = ' ';
                switch (variantName)
                {
                    case nameof(keyTypes.Digits):
                        value = (char)(random.Next(10) + 48);
                        break;
                    case nameof(keyTypes.CapitalLetters):
                        value = (char)(random.Next(26) + 65);
                        break;
                    case nameof(keyTypes.SmallLetters):
                        value = (char)(random.Next(26) + 97);
                        break;
                    case nameof(keyTypes.Chars):
                        var charGroup = random.Next(3);
                        switch (charGroup)
                        {
                            case 0:
                                value = (char)(random.Next(15) + 33);
                                break;
                            case 1:
                                value = (char)(random.Next(7) + 58);
                                break;
                            case 2:
                                value = (char)(random.Next(5) + 91);
                                break;
                        }
                        break;
                }
                tempPassword.Add(value);
            }
            return new string(tempPassword.ToArray());
        }

        private IEnumerable<string> GetVariantTypes(KeyTypes keyTypes)
        {
            var types = keyTypes.GetType();
            var properties = types.GetProperties();
            var resultList = new List<string>();
            foreach(var properyty in properties)
            {
                var value = (bool)properyty.GetValue(keyTypes);
                if (value)
                    resultList.Add(properyty.Name);
            }

            return resultList;
        }
    }
}
