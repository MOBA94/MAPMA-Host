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

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class CustomerController {
        private ICUSTOMER<Customer> DBcus;
        private const int SaltByteLength = 24;
        private const int DerivedKeyLength = 24;

        /// <summary>
        /// the constructor for CustomerController
        /// </summary>
        public CustomerController () {
            DBcus = new DBCustomer();
        }

        /// <summary>
        ///  not implemented yet
        /// </summary>
        /// <param name="cus"></param>
        public void Create(Customer cus) {
            throw new NotImplementedException();
        }


        /// <summary>
        ///  not implemented yet
        /// </summary>
        /// <param name="username"></param>
        public void Delete(string username) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// the method to get a customer
        /// </summary>
        /// <param name="username">customers user name</param>
        /// <returns>a customer</returns>
        public Customer Get(string username) {
            return DBcus.Get(username);
        }

        /// <summary>
        ///  not implemented yet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  not implemented yet
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Customer entity) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// the method for a customer logs in on the web whit the right info
        /// </summary>
        /// <param name="inputPassword">the customers password</param>
        /// <param name="username">customers user name</param>
        /// <returns>a customer if all info are right else null</returns>
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
        /// <summary>
        /// the method to create a new customer
        /// </summary>
        /// <param name="cus">a customer whit all the parametes needene to the customer</param>
        /// <param name="password"> the password the customer have made</param>
        /// <returns></returns>
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

        /// <summary>
        /// the bethod to create a salt to the hassed password
        /// </summary>
        /// <returns>a salt in bytes by the length of 24</returns>
        public static byte[] CreateSalt() {
            var rngCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteLength];
            rngCsp.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password">password for the customer when the customer create a account</param>
        /// <param name="salt"> the salt for another method</param>
        /// <param name="iterationCount">the number of have many times its running the algorithm</param>
        /// <returns></returns>
        public static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount) {
            byte[] hashValue;
            var valueToHash = string.IsNullOrEmpty(password) ? string.Empty : password;
            using (var pbkdf2 = new Rfc2898DeriveBytes(valueToHash, salt, iterationCount)) {
                hashValue = pbkdf2.GetBytes(DerivedKeyLength);
            }
            return hashValue;
                
        }
        /// <summary>
        /// a method there return a number
        /// </summary>
        /// <returns>a number of have many times its running the algorithm </returns>
        public int GetIterationCount() {
            return 24000;
        }

        /// <summary>
        /// the method to mack the hasted password to a string
        /// </summary>
        /// <param name="password">the password from the customer of the customers choice</param>
        /// <returns></returns>
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

        /// <summary>
        /// the method to vertify the password the customer trys to login whit and the password there are in the database
        /// </summary>
        /// <param name="hashedPassword">the password from the database</param>
        /// <param name="inputPassword">the password the customer inputs when customer is logging in</param>
        /// <returns>a true of false if the password are the same or not for that customer</returns>
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

        /// <summary>
        /// the method to compaire the 2 passwords are the same
        /// </summary>
        /// <param name="hashedPassword">the password from the database</param>
        /// <param name="inputPassword">the password the customer inputs when customer is logging in</param>
        /// <returns>a true of false if the password are the same or not for that customer</returns>
        private static bool ConstantTimeCoparison(byte[] inputPassword, byte[] hashedPassword) {
            uint difference = (uint) inputPassword.Length ^ (uint) hashedPassword.Length;
            for (var i = 0; i < inputPassword.Length && i < hashedPassword.Length; i++) {
                 difference |= (uint)(inputPassword[i] ^ hashedPassword[i]);
            }
            return difference == 0;
        }

    }
}

