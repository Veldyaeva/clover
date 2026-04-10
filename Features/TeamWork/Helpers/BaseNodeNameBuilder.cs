using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class BaseNodeNameBuilder
    {
        public static string Build(string nodeGroup, string nodeGroupDetail, string productCategory, string sourceArticul = null)
        {
            string group = StringNormalizer.TrimOrEmpty(nodeGroup);
            string detail = StringNormalizer.TrimOrEmpty(nodeGroupDetail);
            string category = StringNormalizer.TrimOrEmpty(productCategory);
            string articul = StringNormalizer.TrimOrEmpty(sourceArticul);

            if (string.IsNullOrWhiteSpace(group))
            {
                return string.Empty;
            }

            string baseName;
            if (string.IsNullOrWhiteSpace(detail))
            {
                baseName = string.IsNullOrWhiteSpace(category)
                    ? group
                    : $"{group} {category}";
            }
            else
            {
                baseName = string.IsNullOrWhiteSpace(category)
                    ? $"{group} {detail}"
                    : $"{group} {detail} {category}";
            }

            if (string.IsNullOrWhiteSpace(articul))
            {
                return baseName;
            }

            return baseName.Contains(articul, System.StringComparison.CurrentCultureIgnoreCase)
                ? baseName
                : $"{baseName} ({articul})";
        }

        public static string Build(BaseNodeDefinition node)
        {
            return node == null
                ? string.Empty
                : Build(node.NodeGroup, node.NodeGroupDetail, node.ProductCategory, node.SourceArticul);
        }
    }
}
