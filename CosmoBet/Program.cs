using System;
using System.Globalization;

namespace CosmoBet.Completo
{
    internal class Program
    {

        private static string cosmobet = @"
                    ___  _____  ___  __  __  _____  ____  ____  ____ 
                   / __)(  _  )/ __)(  \/  )(  _  )(  _ \( ___)(_  _)
                  ( (__  )(_)( \__ \ )    (  )(_)(  ) _ < )__)   )(  
                   \___)(_____)(___/(_/\/\_)(_____)(____/(____) (__)
        ";

        private static string cosmobetLogin = @"
                              __    __    __  __  _  _ 
                             (  )  /  \  / _)(  )( \( )
                              )(__( () )( (/\ )(  )  ( 
                             (____)\__/  \__/(__)(_)\_)
        ";

        private static string cosmobetCadastro = @"
                       __   __   ___   __   ___  ____  ___   __  
                      / _) (  ) (   \ (  ) / __)(_  _)(  ,) /  \ 
                     ( (_  /__\  ) ) )/__\ \__ \  )(   )  \( () )
                      \__)(_)(_)(___/(_)(_)(___/ (__) (_)\_)\__/  
        ";

        private static string cosmobetMenuDeJogos = @"
           __  __  ___  _  _  _  _    ___  ___      __  __    __   __   ___   
          (  \/  )(  _)( \( )( )( )  (   \(  _)    (  )/  \  / _) /  \ / __) 
           )    (  ) _) )  (  )()(    ) ) )) _)   __)(( () )( (/\( () )\__ \ 
          (_/\/\_)(___)(_)\_) \__/   (___/(___)  (___/ \__/  \__/ \__/ (___/        
        ";

        private static Dictionary<string, Profile> users = new Dictionary<string, Profile>();

        static void Main()
        {
            while (true)
            {
                ShowMenuInicial();
            }
        }

        private static void ShowMenuInicial()
        {
            Console.Clear();
            Console.WriteLine(cosmobet);
            Console.WriteLine("[1] LOGIN");
            Console.WriteLine("[2] CADASTRO");
            Console.WriteLine("[3] SAIR");
            Console.WriteLine();

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Cadastro();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA!");
                    Console.ReadKey();
                    break;
            }
        }

        private static void Login()
        {
            Console.Clear();
            Console.Write(cosmobet);
            Console.WriteLine(cosmobetLogin);

            Console.Write("USUÁRIO: ");
            string username = Console.ReadLine();
            Console.Write("SENHA: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(username) && users[username].Password == password)
            {
                Console.WriteLine("SEJA BEM VINDA(O), " + username);
                Console.WriteLine("\nPRESSIONE QUALQUER TECLA PARA IR AO MENU DE JOGOS");
                Console.ReadKey();

                MenuDeJogos(users[username]);
            }

            else
            {
                Console.WriteLine("USUÁRIO OU SENHA INCORRETOS!");
                Console.ReadKey();
            }
        }

        private static void Cadastro()
        {
            Console.Clear();
            Console.Write(cosmobet);
            Console.WriteLine(cosmobetCadastro);
            Console.Write("INSIRA O USERNAME DESEJADO: ");
            string username = Console.ReadLine();

            if (users.ContainsKey(username))
            {
                Console.WriteLine("USUÁRIO JÁ EM USO!");
                Console.ReadKey();
                return;
            }

            Console.Write("INSIRA A SENHA: ");
            string password = Console.ReadLine();

            Console.Write("INSIRA A DATA DE NASCIMENTO (DD/MM/AAAA): ");
            DateTime birthdate = DateTime.Parse(Console.ReadLine());

            if (users.ContainsKey(username) && users[username].Password == password)
            {
                Profile usuario = users[username];

                int idade = DateTime.Now.Year - usuario.Birthdate.Year;
                if (idade < 18)
                {
                    Console.WriteLine("NÃO PODE JOGAR!");
                    Environment.Exit(0);
                }
            }

            Console.Write("INSIRA O VALOR DO DEPÓSITO INICIAL: ");
            double deposit = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            if (deposit < 0.0)
            {
                Console.Write("VALOR INVÁLIDO, DIGITE NOVAMENTE! ");
            }

            users.Add(username, new Profile(username, password, birthdate, deposit));
            Console.WriteLine("CADASTRO REALIZADO COM SUCESSO!");
            Console.WriteLine("\nPRESSIONE QUALQUER TECLA PARA VOLTAR AO MENU E REALIZE O LOGIN");
            Console.ReadKey();
        }

        private static void MenuDeJogos(Profile usuario)
        {
            Console.Clear();
            Console.Write(cosmobet);
            Console.WriteLine(cosmobetMenuDeJogos);
            Console.WriteLine("\nSALDO ATUAL: " + usuario.InitialDeposit.ToString("C", CultureInfo.InvariantCulture));
            Console.WriteLine("[1] JOGO");
            Console.WriteLine("[2] JOGO");
            Console.WriteLine("[3] VOLTAR AO MENU INICIAL");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.WriteLine("JOGO UM");
                    JogoUm();
                    break;
                case "2":
                    Console.WriteLine("JOGO DOIS");
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA!");
                    break;
            }

            Console.WriteLine("\nPRESSIONE QUALQUER TECLA PARA VOLTAR AO MENU DE JOGOS");
            Console.ReadKey();
            MenuDeJogos(usuario);
        }


        private static void JogoUm()
        {
            Console.Clear();
            Random rnd = new Random();
            int numero = 5;

            Console.WriteLine("VOCÊ TÊM TRÊS TENTATIVAS PARA ADIVINHAR O NÚMERO ");

            for (int i = 0; i < 2; i++)
            {
                Console.Write("\nDIGITE O NÚMERO: ");
                int tentativa = int.Parse(Console.ReadLine());
                if (tentativa != numero)
                    Console.WriteLine($"ERROU! TENTE NOVAMENTE, FALTAM {i} TENTATIVAS");
                tentativa = int.Parse(Console.ReadLine());
            }
        }

        internal class Profile
        {
            private string _username;

            public string Username
            {
                get { return _username; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("O NOME NÃO PODE SER VAZIO");
                    }
                    _username = value;
                }
            }

            private string _password;

            public string Password
            {
                get { return _password; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("A SENHA NÃO PODE SER VAZIA");
                    }
                    _password = value;
                }
            }

            private DateTime _birthdate;
            public DateTime Birthdate
            {
                get { return _birthdate; }
                set
                {
                    if (value == DateTime.MinValue)
                    {
                        throw new ArgumentException("A DATA NÃO PODE SER VAZIA");
                    }
                    _birthdate = value;
                }
            }

            public double InitialDeposit { get; set; }

            public Profile(string username, string password, DateTime birthdate, double deposit)
            {
                Username = username;
                Password = password;
                Birthdate = birthdate;
                InitialDeposit = deposit;
            }
        }
    }
}
