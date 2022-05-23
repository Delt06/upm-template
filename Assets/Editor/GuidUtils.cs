using System;
using System.IO;
using System.Linq;

namespace Editor
{
    public static class GuidUtils
    {
        public static void RegenerateGuids(string rootPath)
        {
            var filePaths = Directory.GetFiles(rootPath, "*.meta", SearchOption.AllDirectories);

            foreach (var filePath in filePaths)
            {
                var metaFileText = File.ReadAllLines(filePath);

                File.WriteAllLines(filePath, metaFileText
                    .Select(l =>
                        {
                            if (!l.StartsWith("guid: ")) return l;

                            var parts = l.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            parts[1] = Guid.NewGuid().ToString("N");
                            return string.Join(" ", parts);
                        }
                    )
                );
            }
        }
    }
}