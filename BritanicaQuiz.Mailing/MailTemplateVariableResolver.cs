namespace BritanicaQuiz.Mailing
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class MailTemplateVariableResolver
    {
        public string ResolveTemplate(string template, IDictionary<string, string> templateVariables)
        {
            foreach (var variable in templateVariables)
            {
                template = ReplaceVariable(template, variable.Key, variable.Value);
            }

            var missingVariables = Regex.Matches(template, "##.*##");
            if (missingVariables.Count != 0)
            {
                var errorResult = "Template has missing variables - ";

                foreach (var item in missingVariables)
                {
                    errorResult += (item.ToString() + ", ");
                }

                errorResult = errorResult.Substring(0, errorResult.Length - 2);

                throw new ArgumentException(errorResult);
            }

            return template;
        }

        private string ReplaceVariable(string template, string variableName, string variableValue)
        {
            var templateWithReplacedValue = Regex.Replace(template, variableName, variableValue, RegexOptions.IgnoreCase);

            if (templateWithReplacedValue == template)
            {
                throw new ArgumentException(string.Format("Failed to replace variable {0} with value {1} - The variable was not found", variableName, variableValue));
            }

            return templateWithReplacedValue;
        }
    }
}
