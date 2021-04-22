using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{

    public class Contacto : List<Contacto>
    {
       
       // public string completo;
        public string nombre;
        public string apellido;
        public string telefono;
        public int edad;

        public string getData()
        {
            return this.nombre + " apellido: " + this.apellido +" Telefono: " +this.telefono+ "  Edad:  " + this.edad;

        }
    }



    class Program
    {
        public static  int indexx = 0;

        static List<Contacto> persona = new List<Contacto>();
        static string path = @"d:\\Agenda\Agenda.txt";
        
        public class Agenda
        {
            public static  void AgregaContacto()
            {

                Console.WriteLine("Ingrese Nombre: ");
                string nombre = Console.ReadLine();

                while(nombre.Length <=3)
                {
                    Console.WriteLine("El nombre debe tener mas de 3 caracteres");
                    Console.WriteLine("Ingrese nombre");
                    nombre = Console.ReadLine();
                }

                Console.WriteLine("Ingrese Apellido: ");
                string apellido = Console.ReadLine();

                while (apellido.Length <= 3)
                {
                    Console.WriteLine("El apellido debe tener mas de 3 caracteres");
                    Console.WriteLine("Ingrese apellido");
                    apellido = Console.ReadLine();
                }

                Console.WriteLine("Ingrese telefono: ");
                string telefono = Console.ReadLine();


                Console.WriteLine("Ingrese edad: ");
                int edad = int.Parse(Console.ReadLine());

                persona.Add(new Contacto() { nombre = nombre, apellido = apellido, telefono = telefono, edad = edad });


                using(StreamWriter sw = File.AppendText(path))
                {              
                    sw.WriteLine(nombre+","+apellido+","+telefono+","+edad);                   
                }

                Console.WriteLine("Contacto Agregado");

            }


            public static void EliminaContacto()
            {
                if (persona.Count > 0)
                {

                    MostrarAgenda();
                    Console.WriteLine("Seleccione el numero de Contacto que desea Eliminar");
                    int pEliminar = Int32.Parse(Console.ReadLine());

                   
                    if (pEliminar <= indexx)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Contacto Eliminado:  {0}  {1} ", persona[pEliminar - 1].nombre.ToUpper(), persona[pEliminar - 1].apellido.ToUpper());
                        Console.ForegroundColor = ConsoleColor.White;
                        persona.RemoveAt(pEliminar - 1);
                   

                        GrabaArchivo();

                    }
                    else
                    {
                        Console.WriteLine("el numero ingresado es incorrecto --   Ingrese Nuevamente");
                    }


                }
                else
                {
                    Console.WriteLine("No existe Ningun Contacto para eliminar!");
                }


            }


            public static void GrabaArchivo()
            {
                File.Delete(path);
               
                using (StreamWriter sw = File.AppendText(path))  
                {
                    foreach (var item in persona)
                    {

                      
                        sw.WriteLine(item.nombre+","+item.apellido+","+item.telefono+","+item.edad);
                    }           
                }             
            }


            public static void MostrarAgenda()
            {
                Console.WriteLine($"Carpeta Actual '{Environment.CurrentDirectory}'");
                path = Environment.CurrentDirectory+"/Agenda.txt";

                persona.RemoveRange(0,persona.Count);
                
                if (!File.Exists(path))
                {
                    Console.WriteLine("No existe Archivo ---   Creando Archivo");

                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                      
                    }
                    if (File.Exists(path))
                        {
                            Console.WriteLine("Archivo Creado");
                        }
                    System.Threading.Thread.Sleep(2000);
                }
               
                string[] readText = File.ReadAllLines(path);
                
                foreach (string s in readText  )
                {
                                    
                    string[] items = s.Split(',');
                   
                        string nom = items[0];
                        string ape = items[1];
                        string tel = items[2];
                        int eda = int.Parse(items[3]);
                   
                    persona.Add(new Contacto() { nombre = nom, apellido = ape, telefono = tel, edad = eda });
                   
                }

                indexx = 0;
                for (int j=0; j< persona.Count; j++)
                {
                    
                    //int index = j + 1;
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("{0}. {1,-20}  {2,-20} {3,-15} {4}",j+1 , persona[j].nombre , persona[j].apellido , persona[j].telefono, persona[j].edad); ;
                    // Console.WriteLine("----------------------------------------------------------");
                    indexx ++;
                }

            }

            public static void EditarContacto()
            {
                Console.WriteLine("Seleccione Contacto a editar ");
                int seleccion = Int32.Parse(Console.ReadLine())-1;


                Console.WriteLine("Ingrese Nombre: ");
                string nombre = Console.ReadLine();
                
                while (nombre.Length <= 3)
                {
                    Console.WriteLine("El nombre debe tener mas de 3 caracteres");
                    Console.WriteLine("Ingrese nombre");
                    nombre = Console.ReadLine();
                   
                }

                Console.WriteLine("Ingrese Apellido: ");
                string apellido = Console.ReadLine();
               

                while (apellido.Length <= 3)
                {
                    Console.WriteLine("El apellido debe tener mas de 3 caracteres");
                    Console.WriteLine("Ingrese apellido");
                    apellido = Console.ReadLine();
                    
                }

                Console.WriteLine("Ingrese telefono: ");
                string telefono = Console.ReadLine();
                

                Console.WriteLine("Ingrese edad: ");
                int edad = int.Parse(Console.ReadLine());
                
                persona[seleccion].nombre = nombre;
                persona[seleccion].apellido = apellido;
                persona[seleccion].telefono = telefono;
                persona[seleccion].edad = edad;


                GrabaArchivo();

            }


            public static void BuscaxApellido(string Busqueda)
            {
            
                Console.WriteLine("por Apellido {0}",Busqueda);

                List<Contacto> resultFindAll = persona.FindAll(
                    delegate (Contacto current)
                    {
                        return current.apellido.ToLower() == Busqueda.ToLower();
                    }
                );

                for (int j = 0; j < resultFindAll.Count; j++)
                {


                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("{0}. {1,-20}  {2,-20} {3,-15} {4}", j + 1, resultFindAll[j].nombre, resultFindAll[j].apellido, resultFindAll[j].telefono, resultFindAll[j].edad); ;

                }

            }


            public static void BuscaxNombre(string Busqueda)
            {
                Console.WriteLine("por Nombre {0}",Busqueda);

                List<Contacto> resultFindAll = persona.FindAll(
                    delegate(Contacto current) 
                    {
                        return current.nombre.ToLower() == Busqueda.ToLower();
                    }
                );

                for (int j = 0; j < resultFindAll.Count; j++)
                {

                    
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("{0}. {1,-20}  {2,-20} {3,-15} {4}", j + 1, resultFindAll[j].nombre, resultFindAll[j].apellido, resultFindAll[j].telefono, resultFindAll[j].edad); ;

                }

            }


            public static void Buscar()
            {
                Console.WriteLine("1. Buscar por Apellido.");
                Console.WriteLine("2. Buscar por Nombre.");
                Console.WriteLine("Seleccione opcion: ");

                int selec = int.Parse( Console.ReadLine());
                string busqueda;
                switch (selec)
                {
                    case 1:
                        Console.WriteLine("Ingrese Apellido a buscar: ");
                         busqueda = Console.ReadLine();
                        BuscaxApellido( busqueda);
                        Menu();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese Nombre a buscar: ");
                         busqueda = Console.ReadLine();
                        BuscaxNombre(busqueda);
                        Menu();
                        break;
                    

                    default:
                        MostrarAgenda();
                        Menu();

                        break;
                }
            }

            public static void Menu()
            {
                Console.WriteLine("");
                Console.WriteLine("--------Menu--------");
                Console.WriteLine("1. Agregar Contacto.");
                Console.WriteLine("2. Mostrar Contactos.");
                Console.WriteLine("3. Editar Contacto.");
                Console.WriteLine("4. Eliminar Contacto");
                Console.WriteLine("5. Buscar Contacto");

                Console.WriteLine("Seleccione Opcion:");


                int seleccion = Int32.Parse(Console.ReadLine());

                switch (seleccion)
                {
                    case 1:
                        Console.WriteLine("Agrega Contacto..");
                        AgregaContacto();
                        Console.Clear();
                        MostrarAgenda();
                        Menu();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarAgenda();
                        Menu();
                        break;
                    case 3:
                        Console.WriteLine("Editar Contacto");
                        MostrarAgenda();
                        EditarContacto();
                        MostrarAgenda();
                        Menu();
                        break;
                    case 4:
                        EliminaContacto();
                        MostrarAgenda();
                        Menu();
                        break;
                    case 5:
                        Buscar();
                        break;

                    default:
                        MostrarAgenda();
                        Menu();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            
            Agenda.MostrarAgenda();
            Agenda.Menu();

        }
    }
}
