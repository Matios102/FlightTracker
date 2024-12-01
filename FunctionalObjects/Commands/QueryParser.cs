using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExCSS;


// Purpose: This class is responsible for parsing the query
namespace FlightProject.FunctionalObjects.Commands
{
    public class QueryParser
    {
        public static Command Display(string querry)
        {
            // Display pattern : display {fields} from {object_class} where {conditions}
            string pattern = "(display){1}\\s(.*)\\sfrom(?:\\s(\\w+){0,1}){0,1}\\s{0,1}(?:where\\s(.*)){0,1}";
            var match = new Regex(pattern).Match(querry);
            if (match.Success)
            {
                var command = new Command
                {
                    type = "display",
                    object_class = match.Groups[3].Value
                };
                if (match.Groups[3].Success)
                {
                    command.object_fileds = match.Groups[2].Value.Split(',').ToList();
                }
                if (match.Groups[4].Success)
                {
                    command.conditions = new List<Condition>();
                    List<string> fields = new List<string>();
                    List<string> operators = new List<string>();
                    List<string> values = new List<string>();
                    List<string> logical_operators = new List<string>();
                    var conditions = match.Groups[4].Value.Split(' ');
                    for (int i = 0; i < conditions.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            fields.Add(conditions[i]);
                        }
                        else if (i % 4 == 1)
                        {
                            operators.Add(conditions[i]);
                        }
                        else if (i % 4 == 2)
                        {
                            values.Add(conditions[i]);
                        }
                        else if (i % 4 == 3)
                        {
                            logical_operators.Add(conditions[i]);
                        }
                    }
                    for (int i = 0; i < fields.Count; i++)
                    {
                        command.conditions.Add(new Condition
                        {
                            property = fields[i],
                            oprator = operators[i],
                            value = values[i]
                        });
                    }
                    command.logical_operators = logical_operators;
                }
                return command;
            }
            else
            {
                throw new Exception("Invalid Query");
            }
        }

        public static Command Update(string querry)
        {
            // Update pattern : update {object_class} set ({key}={value}, {key}={value}) where {conditions}
            string pattern = "update\\s(\\w+)\\sset\\s\\((.*)\\)\\s{0,1}(?:where\\s(.*)){0,1}";
            var match = new Regex(pattern).Match(querry);
            if (match.Success)
            {
                var command = new Command
                {
                    type = "update",
                    object_class = match.Groups[1].Value,
                };
                command.key_value = new Dictionary<string, string>();
                var set = match.Groups[2].Value;
                foreach (string s in set.Split(", "))
                {
                    var parts = s.Split("=");
                    command.key_value.Add(parts[0], parts[1]);
                }
                command.conditions = new List<Condition>();
                List<string> fields = new List<string>();
                List<string> operators = new List<string>();
                List<string> values = new List<string>();
                List<string> logical_operators = new List<string>();
                var conditions = match.Groups[3].Value.Split(' ');
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        fields.Add(conditions[i]);
                    }
                    else if (i % 4 == 1)
                    {
                        operators.Add(conditions[i]);
                    }
                    else if (i % 4 == 2)
                    {
                        values.Add(conditions[i]);
                    }
                    else if (i % 4 == 3)
                    {
                        logical_operators.Add(conditions[i]);
                    }
                }
                for (int i = 0; i < fields.Count; i++)
                {
                    command.conditions.Add(new Condition
                    {
                        property = fields[i],
                        oprator = operators[i],
                        value = values[i]
                    });
                }
                command.logical_operators = logical_operators;
                return command;
            }
            else
            {
                throw new Exception("Invalid Query");
            }
        }

        public static Command Add(string querry)
        {
            // Add pattern : add {object_class} new ({key}={value}, {key}={value})
            string pattern = "(?:add)\\s(\\w+)\\s(?:new)\\s\\((.*)\\)";
            var match = new Regex(pattern).Match(querry);
            if (match.Success)
            {
                var command = new Command
                {
                    type = "add",
                    object_class = match.Groups[1].Value,
                    key_value = new Dictionary<string, string>()
                };
                var set = match.Groups[2].Value;
                foreach (string s in set.Split(", "))
                {
                    var parts = s.Split("=");
                    command.key_value.Add(parts[0], parts[1]);
                }
                return command;
            }
            else
            {
                throw new Exception("Invalid Query");
            }
        }

        public static Command Delete(string querry)
        {
            // Delete pattern : delete {object_class} where {conditions}
            string pattern = "(delete){1}\\s(\\w+)\\s{0,1}(?:where\\s(.*)){0,1}";
            var match = new Regex(pattern).Match(querry);
            if(match.Success)
            {
                var command = new Command
                {
                    type = "delete",
                    object_class = match.Groups[2].Value
                };
                command.conditions = new List<Condition>();
                List<string> fields = new List<string>();
                List<string> operators = new List<string>();
                List<string> values = new List<string>();
                List<string> logical_operators = new List<string>();
                var conditions = match.Groups[3].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in conditions)
                {

                }
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        fields.Add(conditions[i]);
                    }
                    else if (i % 4 == 1)
                    {
                        operators.Add(conditions[i]);
                    }
                    else if (i % 4 == 2)
                    {
                        values.Add(conditions[i]);
                    }
                    else if (i % 4 == 3)
                    {
                        logical_operators.Add(conditions[i]);
                    }
                }
                for (int i = 0; i < fields.Count(); i++)
                {
                    command.conditions.Add(new Condition
                    {
                        property = fields[i],
                        oprator = operators[i],
                        value = values[i]
                    });
                }
                command.logical_operators = logical_operators;
                return command;
            }
            else
            {
                throw new Exception("Invalid Query");
            }
        }

    }
}
