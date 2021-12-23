using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CsvParser
{
    public partial class Form1 : Form
    {
        public string path = @"D:\OneDrive\CsvParser\CsvParser\CsvParser.txt";
        List<Person> persons = new List<Person>();
        public string CsvToSave = "Name;Age;Gender;School";
        public Form1()
        {
            InitializeComponent();
        }
        private async Task SavePerson() 
        {
            string csv = "Name;Age;Gender;School";
            foreach(Person person in persons)
            {
                csv +=  Environment.NewLine + string.Join(";",person.Name,person.Age,person.Gender,person.School);
            }
            File.WriteAllText(path, csv);
            
        }
        private async Task btnAddChange()
        {
            if (txtNm.Text != "" & nmAge.Value != 0 & cbGender.Text != "" & txtSchool.Text != "")
            {
                btnAdd.Enabled = true;
                btnAdd.Text = "Add";
            }
            else
            {
                btnAdd.Enabled = false;
                btnAdd.Text = "Haha...";
            }
           
        }
        private async Task btnRmChange()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                btnRemove.Enabled = false;
                btnRemove.Text = "Bye...";
            }
            else
            {
                btnRemove.Enabled = true;
                btnRemove.Text = "Remove";
            }
        }


        private async  void btnSave_Click(object sender, EventArgs e)
        {
            
            await SavePerson();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
            cbGender.Items.Add(Person.Genders.Male);
            cbGender.Items.Add(Person.Genders.Female);
            string[] lines = File.ReadAllLines(path);
            string header = lines[0];
            string[] splittedHeader = header.Split(';');
            int idxName = -1, idxAge = -1, idxGender = -1, idxSchool = -1;
            for (int i = 0; i < splittedHeader.Length; i++)
            {
                if (splittedHeader[i].ToLower() == "name")
                {
                    idxName = i;
                }
                else if (splittedHeader[i].ToLower() == "age")
                {
                    idxAge = i;
                }
                else if (splittedHeader[i].ToLower() == "gender")
                {
                    idxGender = i;
                }
                else if (splittedHeader[i].ToLower() == "school")
                {
                    idxSchool = i;
                }
            }

            for (int j = 1; j < lines.Length; j++)
            {
                string[] lineSplitted = lines[j].Split(';');
                Person person = new Person
                {
                    Name = lineSplitted[idxName],
                    Age = int.Parse(lineSplitted[idxAge]),
                    Gender = (Person.Genders)Enum.Parse(typeof(Person.Genders), lineSplitted[idxGender]),
                    School = lineSplitted[idxSchool]
                };
                persons.Add(person);

            }

            foreach (var person in persons)
            {
                var item = new ListViewItem();
                item.Text = person.Name;
                item.SubItems.Add(Convert.ToString(person.Age));
                item.SubItems.Add(Convert.ToString(person.Gender));
                item.SubItems.Add(person.School);
                listView1.Items.Add(item);
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            string name = txtNm.Text;
            int age = Convert.ToInt32(nmAge.Value);
            Person.Genders gender = (Person.Genders)cbGender.SelectedItem;
            string school = txtSchool.Text;
            string csv = Environment.NewLine + string.Join(";", name, age, gender, school);
            CsvToSave += csv;
            var item = new ListViewItem();
            item.Text = name;
            item.SubItems.Add(age.ToString());
            item.SubItems.Add(gender.ToString());
            item.SubItems.Add(school);
            listView1.Items.Add(item);
            persons.Add(
                new Person
                {
                    Age = age,
                    Gender = gender,
                    Name = name,
                    School = school
                }
            );
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            
            if (listView1.SelectedItems.Count > 0)
            {
                int indexToRemove = listView1.SelectedItems[0].Index;
                listView1.Items.RemoveAt(indexToRemove);
                persons.RemoveAt(indexToRemove);
            
                
                
                
            }
        }

        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await btnRmChange();
        }

        private async void txtNm_TextChanged(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
        }

        private async void nmAge_ValueChanged(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
        }

        private async void cbGender_SelectedValueChanged(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
        }

        private async void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
        }

        private async void txtSchool_TextChanged(object sender, EventArgs e)
        {
            await btnAddChange();
            await btnRmChange();
        }
    }   
}


