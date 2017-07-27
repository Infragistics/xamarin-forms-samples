namespace Moo2U.Model {
    using System;
    using Prism.Mvvm;
    using SQLite.Net.Attributes;

    public class Product : BindableBase {

        String _description;
        String _icon;
        Int32 _id;
        Double _price;
        String _unitOfMeasure;

        [MaxLength(50), NotNull]
        public String Description {
            get { return _description; }
            set {
                _description = value;
                RaisePropertyChanged();
            }
        }

        [MaxLength(50), NotNull]
        public String Icon {
            get { return _icon; }
            set {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        [PrimaryKey, AutoIncrement]
        public Int32 Id {
            get { return _id; }
            set {
                _id = value;
                RaisePropertyChanged();
            }
        }

        [NotNull]
        public Double Price {
            get { return _price; }
            set {
                _price = value;
                RaisePropertyChanged();
            }
        }

        [MaxLength(10), NotNull]
        public String UnitOfMeasure {
            get { return _unitOfMeasure; }
            set {
                _unitOfMeasure = value;
                RaisePropertyChanged();
            }
        }

        public Product() {
        }

    }
}
