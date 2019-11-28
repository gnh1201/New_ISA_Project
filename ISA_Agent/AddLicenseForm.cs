using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISA_Agent
{
    public partial class AddLicenseForm : Form
    {
        private LiteDatabase db = new LiteDatabase(@"ISA.db");

        public AddLicenseForm()
        {
            InitializeComponent();

            List<BundleModel> bundles = BundleService.GetInstalledBundles();
            foreach(BundleModel bundle in bundles)
            {
                checkedListBox1.Items.Add(bundle);
            }

            checkedListBox1.DisplayMember = "DisplayName";
        }

        private void BtnNotListed_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("프로그램 이름", "프로그램 이름을 입력하여 주세요.");
            if(!string.IsNullOrEmpty(promptValue)) {
                BundleModel bundle = new BundleModel
                {
                    Publisher = "Unknown",
                    DisplayName = promptValue
                };
                checkedListBox1.Items.Add(bundle, true);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            foreach (BundleModel bundle in checkedListBox1.CheckedItems)
            {
                var licenses = db.GetCollection<LicenseModel>("licenses");
                var license = new LicenseModel
                {
                    DisplayName = bundle.DisplayName,
                    Publisher = bundle.Publisher,
                    RenewType = "Unlimited",
                    PurchaseDate = new DateTime(),
                    ExpireDate = new DateTime()
                };
                licenses.Insert(license);
            }
            Close();
        }
    }
}
