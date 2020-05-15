using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    public class CustomerController {
        private ICUSTOMER<Customer> DBcus;
        private const int SaltByteLength = 24;
        private const int DerivedKeyLength = 24;

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
            try {
                Customer cus = DBcus.Get(username);
                if (VerifyHashedPassword(cus.password, inputPassword) == true) {
                    return cus;
                }
                else {
                    return null;
                }
            }
            catch (ArgumentNullException ANE) {
                return null;
            }
            
        }

        public int Register(Customer cus, string password) {
            int i;

            if (Get(cus.username).username != null) {
                i = 0;
            }
            else {
                try {
                    cus.password = PasswordHashAndSalt(password);
                    DBcus.Register(cus);
                    i = 1;
                }
                catch (Exception e) {
                    i = 2;
                }
            }
            
            return i;
        }

        public static byte[] CreateSalt() {
            var rngCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteLength];
            rngCsp.GetBytes(salt);
            return salt;
        }

        public static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount) {
            byte[] hashValue;
            var valueToHash = string.IsNullOrEmpty(password) ? string.Empty : password;
            using (var pbkdf2 = new Rfc2898DeriveBytes(valueToHash, salt, iterationCount)) {
                hashValue = pbkdf2.GetBytes(DerivedKeyLength);
            }
            return hashValue;
                
        }

        public int GetIterationCount() {
            return 24000;
        }

        [SecuritySafeCritical]
        public string PasswordHashAndSalt(string password) {
            var salt = CreateSalt();
            var iterationCount = GetIterationCount();
            var hashValue = GenerateHashValue(password, salt, iterationCount);
            var iterationCountBtyeArr = BitConverter.GetBytes(iterationCount);
            var valueToSave = new byte[SaltByteLength + DerivedKeyLength + iterationCountBtyeArr.Length];
            Buffer.BlockCopy(salt, 0, valueToSave, 0, SaltByteLength);
            Buffer.BlockCopy(hashValue, 0, valueToSave, SaltByteLength, DerivedKeyLength);
            Buffer.BlockCopy(iterationCountBtyeArr, 0, valueToSave, salt.Length + hashValue.Length, iterationCountBtyeArr.Length);
            return Convert.ToBase64String(valueToSave);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string inputPassword) {
            
                var salt = new byte[SaltByteLength];
                var actualPasswordByteArr = new byte[DerivedKeyLength];
                var actualSavedHashResulteByteArr = Convert.FromBase64String(hashedPassword);

                var iterationCountLength = actualSavedHashResulteByteArr.Length - ( salt.Length + actualPasswordByteArr.Length );
                var iterationCountByteArr = new byte[iterationCountLength];
                Buffer.BlockCopy(actualSavedHashResulteByteArr, 0, salt, 0, SaltByteLength);
                Buffer.BlockCopy(actualSavedHashResulteByteArr, SaltByteLength, actualPasswordByteArr, 0, actualPasswordByteArr.Length);
                Buffer.BlockCopy(actualSavedHashResulteByteArr, ( salt.Length + actualPasswordByteArr.Length ), iterationCountByteArr, 0, iterationCountLength);
                var inputPasswordByteArr = GenerateHashValue(inputPassword, salt, BitConverter.ToInt32(iterationCountByteArr, 0));
                return ConstantTimeCoparison(inputPasswordByteArr, actualPasswordByteArr);
        }

        private static bool ConstantTimeCoparison(byte[] inputPassword, byte[] hashedPassword) {
            uint difference = (uint) inputPassword.Length ^ (uint) hashedPassword.Length;
            for (var i = 0; i < inputPassword.Length && i < hashedPassword.Length; i++) {
                 difference |= (uint)(inputPassword[i] ^ hashedPassword[i]);
            }
            return difference == 0;
        }

    }
}

