using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    public class CustomerController {
        private ICUSTOMER<Customer> DBcus;

        public CustomerController () {
            DBcus = new DBCustomer();
        }
        public void Create(Customer cus) {
            throw new NotImplementedException();
        }

        public void Delete(string username) {
            throw new NotImplementedException();
        }

        public Customer Get(string username) {
            return DBcus.Get(username);
        }

        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Customer entity) {
            throw new NotImplementedException();
        }

        public Customer Login(string inputPassword, string username) {
            Customer cus = DBcus.Get(username);

            if (VerifyHashedPassword(cus.password, inputPassword, cus.salt) == true){
                return cus;
            }
            else {
                return null;
            }
        }

        public void Register(Customer cus, string password) {
            cus.salt = CreateSalt();
            cus.password = PasswordHashAndSalt(password, cus.salt);
            DBcus.Register(cus);
        }

        public static string CreateSalt() {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[100];
            rngCsp.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string PasswordHashAndSalt(string password, string salt) {

            var password_byes = ASCIIEncoding.ASCII.GetBytes(password);

            byte[] data_input = new byte[salt.Length + password_byes.Length];

            SHA512 shaM = new SHA512Managed();
            var hashed_byte_array = shaM.ComputeHash(data_input);

            string hashed_result = Convert.ToBase64String(hashed_byte_array);

            return hashed_result;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password, string salt) {

            string newHashedPin = PasswordHashAndSalt(password, salt);
            if (newHashedPin.Equals(hashedPassword)) {
                return true;
            }
            else {
                return false;
            }
        }

    }
}

