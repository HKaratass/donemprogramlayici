using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DONEMPROGRAMLAYICI
{
    internal class Settings
    {
        static public int notSayisi = 1;
        static public byte dersSayisi = 1;
        static public int darkModeSeviye = 1;
        static public int gozSagligi = 1;
        static public int donemGunSayisi = 22;

        static public int dersProgCmts = 1;
        static public int dersProgPzr = 1;
        static public byte dersProgIlkBaslayanDersSaati = 8;
        static public byte dersProgIlkBaslayanDersDakika = 30;
        static public byte dersProgSonBitenDersSaati = 16;
        static public byte dersProgSonBitenDersDakika = 30;
        // GEÇİCİ DEĞERLER



        static public void fill()
        {
            notSayisi = Convert.ToInt32(GoogleSQL.settingsVeri[3][2].ToString());
            dersSayisi = Convert.ToByte(GoogleSQL.settingsVeri[4][2].ToString());
            darkModeSeviye = Convert.ToByte(GoogleSQL.settingsVeri[5][2].ToString());
            gozSagligi = Convert.ToByte(GoogleSQL.settingsVeri[6][2].ToString());

            donemGunSayisi = Convert.ToByte(GoogleSQL.settingsVeri[8][2].ToString());

            dersProgCmts = Convert.ToByte(GoogleSQL.settingsVeri[10][2].ToString());
            dersProgPzr = Convert.ToByte(GoogleSQL.settingsVeri[11][2].ToString());

            if (!GoogleSQL.settingsVeri[12][2].ToString().Equals("#N/A")) //GEÇİCİ ÇÖZÜM
            {
                dersProgIlkBaslayanDersSaati = Convert.ToByte(GoogleSQL.settingsVeri[12][2].ToString());
                dersProgIlkBaslayanDersDakika = Convert.ToByte(GoogleSQL.settingsVeri[13][2].ToString());
                dersProgSonBitenDersSaati = Convert.ToByte(GoogleSQL.settingsVeri[14][2].ToString());
                dersProgSonBitenDersDakika = Convert.ToByte(GoogleSQL.settingsVeri[15][2].ToString());
            }
            else
            {
                dersProgIlkBaslayanDersSaati = 9;
                dersProgIlkBaslayanDersDakika = 0;
                dersProgSonBitenDersSaati = 16;
                dersProgSonBitenDersDakika = 0;
            }
            

        }









    }
}
