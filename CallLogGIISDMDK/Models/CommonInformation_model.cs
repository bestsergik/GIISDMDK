using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CallLogGIISDMDK.Models
{
    class CommonInformation_model : INotifyPropertyChanged
    {

        private string _generalInformation;
        private string _titleInformation;
        private string _reduction;
        private string _decryption;

        List<string> generalInformation = new List<string>();
        List<string> reductions = new List<string>();

        public    CommonInformation_model()
        {
            FillGeneralInformation();
            FillReductions();
        }

        private void FillReductions()
        {
            generalInformation.Add("Сайт ГИИС ДМДК");
            generalInformation.Add("https://dmdk.goznak.ru/");

            generalInformation.Add("Телефон тех. поддержки ГИИС ДМДК");
            generalInformation.Add("+7 (495) 665-45-01");

            generalInformation.Add("Почта тех. поддержки ГИИС ДМДК");
            generalInformation.Add("dmdk@goznak.ru");

            generalInformation.Add("Информация для работы ГИИС ДМДК");
            generalInformation.Add("https://goznak.ru/products/9215/");

            generalInformation.Add("Записи проведенных вебинаров");
            generalInformation.Add("доступны по ссылке http://www.youtube.com/playlist?list=PLq7Ai2bwje7DvucHeRdwjLn4su-5DdUQO , которую можно найти на странице https://goznak.ru/products/9215/ , перед списком установок для работы с ГИИС ДМДК.");

            generalInformation.Add("Рукодосдоство пользователя ГИИС ДМДК");
            generalInformation.Add("доступно для скачивания, внизу страницы https://goznak.ru/products/9215/.");

           
        }

        private void FillGeneralInformation()
        {
            reductions.Add("ГИИС ДМДК");
            reductions.Add("Государственная интегрированная информационная система в сфере контроля за оборотом драгоценных металлов, драгоценных камней и изделий из них.");
          
            reductions.Add("МРУ ФПП");
            reductions.Add("Межрегиональное управление федеральной пробирной палаты.");

            reductions.Add("Именник");
            reductions.Add("оттиск клейма изготовителя, содержащий индивидуальные знаки изготовителя и год изготовления изделия. Именники обязаны иметь и ставить их на свои изделия все организации и индивидуальные предприниматели. Знаки именников регистрируются и утверждаются Министерством финансов Российской Федерации (Пробирной палатой) ежегодно.");

            reductions.Add("ОКВЭД");
            reductions.Add("общероссийского классификатора видов экономической деятельности.");

            reductions.Add("Аффина́ж");
            reductions.Add("металлургический процесс очистки некоторых тяжёлых металлов от примесей.");

            reductions.Add("Опробование");
            reductions.Add("комплекс операций по отбору проб и подготовке их к анализу для контроля основных характеристик сырья (полезных ископаемых, продуктов их обогащения и вспомогательных материалов, используемых при обогащении).");

            reductions.Add("СКЗИ");
            reductions.Add("средство криптографической защиты информации.");

            reductions.Add("ЕГРИП");
            reductions.Add("единый государственный реестр индивидуальных предпринимателей.");

            reductions.Add("ОГРН");
            reductions.Add("основной государственный регистрационный номер.");

            reductions.Add("ОГРНИП");
            reductions.Add("основной государственный регистрационный номер индивидуального предпринимателя.");

            reductions.Add("Бенефициар");
            reductions.Add("лицо, которое является получателем денежных средств, и в адрес которого осуществляется денежный платеж.");

        }

        public string GeneralInformation
        {
            get { return _generalInformation; }
            set
            {
                _generalInformation = value;
                OnPropertyChanged("GeneralInformation");
            }
        }

        public string TitleInformation
        {
            get { return _titleInformation; }
            set
            {
                _titleInformation = value;
                OnPropertyChanged("TitleInformation");
            }
        }

        internal List<string> GetReductions()
        {
            return reductions;
        }

        internal List<string> GetGeneralInformation()
        {
            return generalInformation;

        }

        public string Reduction
        {
            get { return _reduction; }
            set
            {
                _reduction = value;
                OnPropertyChanged("Reduction");
            }
        }
        public string Decryption
        {
            get { return _decryption; }
            set
            {
                _decryption = value;
                OnPropertyChanged("Decryption");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
