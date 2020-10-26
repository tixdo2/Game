#include <iostream>
using namespace std;

int main()
{
  int mas[4][5];
  int column = -1;
  int s = 0;
  int preS = -1;
  int row = 0;
  bool NoNegat = true;
  cout << "Vvedite massiv" << '\n';
  for (int i = 0; i < 4; i++)
    for (int j = 0; j < 5; j++)
    {
      cin >> mas[i][j];
    }

  cout<<"Iznachal'niy massiv:" << '\n';

  for (int i = 0; i < 4; i++)
  {
    cout << '\n';
    for (int j = 0; j < 5; j++)
    {
      cout << mas[i][j] << " ";
    }
  }
    
  for (int j = 0; j < 5; j++)
  {
    
    for (int i = 0; i < 4; i++)
    {
      if (mas[i][j] < 0)
      {
        NoNegat = false;
        break;
      }
      if (NoNegat)
      {
        column = i;
        break;
      }
    }
  }
  /*
  for (int l = 0; l < 3; l++)
  {
    for (int i = 0; i < 4; i++)
    {
      for (int j = 0; j < 5; j++)
      {
        s++;
      }
      if (preS > s)
      {
        for (int k = 0; k < 5; k++)
        {
          int el = mas[i][k];
          mas[i][k] = mas[i - 1][k];
          mas[i - 1][k] = el;
        }
      }
      preS = s;
    }
  }

  cout << '\n' << "Rezul'tat:" << '\n';

  for (int i = 0; i < 4; i++)
  {
    cout << '\n';
    for (int j = 0; j < 5; j++)
    {
      cout << mas[i][j] << " ";
    }
  }
  */
  if (column == -1)
  {
    cout << '\n' << "Polozhitel'nih stolbcov net!" << '\n';
  }
  else
  {
    cout << '\n' << "Perviy stolbec bez negativnih: " << column+1;
  }
}