using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Models;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal static class BaseNodeNameBuilder
    {
        public static string Build(string nodeGroup, string nodeGroupDetail, string productCategory)
        {
            string group = StringNormalizer.TrimOrEmpty(nodeGroup);
            string detail = StringNormalizer.TrimOrEmpty(nodeGroupDetail);
            string category = StringNormalizer.TrimOrEmpty(productCategory);

            if (string.IsNullOrWhiteSpace(group))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(detail))
            {
                return string.IsNullOrWhiteSpace(category)
                    ? group
                    : $"{group} {category}";
            }

            return string.IsNullOrWhiteSpace(category)
                ? $"{group} {detail}"
                : $"{group} {detail} {category}";
        }

        public static string Build(BaseNodeDefinition node)
        {
            return node == null
                ? string.Empty
                : Build(node.NodeGroup, node.NodeGroupDetail, node.ProductCategory);
        }
    }
}
