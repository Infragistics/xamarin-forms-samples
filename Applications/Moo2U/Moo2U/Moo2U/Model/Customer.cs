namespace Moo2U.Model {
    using System;
    using Prism.Mvvm;
    using SQLite;

    public class Customer : BindableBase {

        String _address;
        String _addressType;
        String _cityStateZip;
        Int32 _id;
        String _name;

        [MaxLength(64), NotNull]
        public String Address {
            get { return _address; }
            set {
                _address = value;
                RaisePropertyChanged();
            }
        }

        [MaxLength(9), NotNull]
        public String AddressType {
            get { return _addressType; }
            set {
                _addressType = value;
                RaisePropertyChanged();
            }
        }

        [MaxLength(64), NotNull]
        public String CityStateZip {
            get { return _cityStateZip; }
            set {
                _cityStateZip = value;
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

        [MaxLength(50), NotNull]
        public String Name {
            get { return _name; }
            set {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public Customer() {
        }

        public override string ToString() {
            return $"{this.Id} {this.Name} {this.AddressType} {this.Address} {this.CityStateZip}";
        }

    }
}
