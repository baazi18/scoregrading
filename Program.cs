using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace filestreamer
{
	class Program
	{
		static void Main(string[] args)
		{

			filestreamerxc fileobj = new filestreamerxc();
			fileobj.getdatafromfile();

		}
	}


	public class filestreamerxc
	{
		// Method to read data from a text file and stored in a string array.
		public void getdatafromfile()
		{
			string[] lines = System.IO.File.ReadAllLines(@"/Users/pratikbhajekar/Desktop/scores.txt");

			// Display the file contents by using a foreach loop.
			System.Console.WriteLine("Contents of datainlines.txt = ");
			foreach (string line in lines)
			{
				// Use a tab to indent each line of the file.
				Console.WriteLine("\t" + line);
			}

			// Create a data table of 3 columns to store data.
			DataTable dt = new DataTable();

			dt.Columns.Add("Lastname");
			dt.Columns.Add("Firstname");
			dt.Columns.Add("Marks");
			DataRow row;

                       // Populate the data table.
			foreach (string item in lines)
			{
				string[] cons = item.Split(',');
				row = dt.NewRow();

				for (int cIndex = 0; cIndex < 3; cIndex++)
				{
					row[cIndex] = cons[cIndex];
				}

				dt.Rows.Add(row);
			}

			// Create a data view of a data table to perform sort operations.
			DataView dv = dt.DefaultView;
			dv.Sort = "Lastname ASC, Marks DESC";
			DataTable sortedDT = dv.ToTable();
			dt = sortedDT;
			
			// Create a new tet file.
			System.IO.StreamWriter file = new System.IO.StreamWriter(@"/Users/pratikbhajekar/Desktop/graded-scores.txt");
			
			int i;
			
			//Write the data table content to new text file.
           		file.Write(Environment.NewLine);
			foreach (DataRow rows in dt.Rows)
			{
				object[] array = rows.ItemArray;
				for (i = 0; i < array.Length - 1; i++)
				{
                    	file.Write(array[i].ToString() + " | ");
				}
                	file.WriteLine(array[i].ToString());
			}

			 file.Write("*****END OF DATA****" + DateTime.Now.ToString());
			 file.Flush();
			 file.Close();

			Console.WriteLine("File is created on the given path. Now press any key to exit.");
			System.Console.ReadKey();

		}

	}

}
