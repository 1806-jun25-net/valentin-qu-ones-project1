using Pizza.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Main
{
    public class XMLMethods
    {
        

        Task<IEnumerable<Orders>> desListTask = DeserializeFromFileAsync(@"C:\Users\Revature\Desktop\data.xml");

        // Method to deserialize a file from xlm format.
        public async static Task<IEnumerable<Orders>> DeserializeFromFileAsync(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Orders>));

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0; // reset "cursor" of stream to beginning
                return (List<Orders>)serializer.Deserialize(memoryStream);
            }
        }

        //Method to serialize(convert) a file to xml format.
        public static void SerializeToFile(string fileName, List<Orders> people)
        {
            
            var serializer = new XmlSerializer(typeof(List<Orders>));
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fileStream, people);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Path {fileName} was too long! {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Some other error with file I/O: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; // re-throws the same exception
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }


        }

    }
}
