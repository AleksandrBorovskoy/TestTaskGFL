using System.Reflection;
using TestTaskGFL.Entities;

namespace TestTaskGFL.Services
{
    public class TxtDeserializer
    {
        public static CarEntity ReadTxtFile(string path)
        {
            var car = new CarEntity();

            using (var reader = new StreamReader(path))
            {
                string[] splittedLine;
                string? line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    splittedLine = line.Split(':');
                    var properties = new PropertyInfo[splittedLine.Length - 1];
                    var propertyValues = new List<object> { car };
                    var propertyTypes = new List<Type> { typeof(CarEntity) };

                    for (int i = 0; i < splittedLine.Length - 1; i++)
                    {
                        properties[i] = propertyTypes[i].GetProperty(splittedLine[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                        if (i == splittedLine.Length - 2)
                        {
                            if (properties[i].PropertyType == typeof(double))
                            {
                                properties[i].SetValue(propertyValues[i], Convert.ToDouble(splittedLine[i + 1]));
                            }
                            else
                            {
                                properties[i].SetValue(propertyValues[i], splittedLine[i + 1]);
                            }
                        }
                        propertyValues.Add(properties[i].GetValue(propertyValues[i]));
                        propertyTypes.Add(propertyValues[i + 1].GetType());
                    }
                }
            }

            return car;
        }
    }
}
