using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class ConfirmationListItem
    {
        public ConfirmationListItem()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Read_x0020_and_x0020_Sign_x0020_ConfirmationListItem";
        }

        public Metadata __metadata { get; set; }
    }
}
