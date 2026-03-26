using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Matrix.Class
{
    public class CreateArticleMatrix
    {
        string _nameArticle = String.Empty;
        int _id_manager = 0;
        string _nn = String.Empty;
        string _counterArticle = String.Empty;

        public CreateArticleMatrix(string nn)
        {
            _nn = nn;
            _id_manager = Convert.ToInt32(nn.Substring(6,2));
        }

        public string getSeazon()
        {
            //TODO: add logic for seazon
            return "3";
        }


    }
}
