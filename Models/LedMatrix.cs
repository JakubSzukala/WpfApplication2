using System;
using System.Collections.Generic;
using System.Text;


namespace WpfApplication2.Models
{
    internal enum RGB
    {
        R,
        G,
        B
    }
    class LedMatrix
    {
        private int _dimX;

        public int dimX
        {
            get { return _dimX; }
            set { _dimX = value; }
        }

        private int _dimY;
        public int dimY
        {
            get { return _dimY; }
            set { _dimY = value; }
        }

        private int[,,] matrix;

        public LedMatrix(int dimX, int dimY)
        {
            _dimX = dimX;
            _dimY = dimY;

            // initialize matrix with default values
            matrix = new int[_dimX, _dimY, 3];
            for (int i = 0; i < _dimX; i++)
            {
                for (int k = 0; k < _dimY; k++)
                {
                    matrix[i, k, (int)RGB.R] = 0;
                    matrix[i, k, (int)RGB.G] = 0;
                    matrix[i, k, (int)RGB.B] = 0;
                }
            }
        }
        public void mSetColor(int x, int y, int r, int g, int b)
        {
            matrix[x, y, (int)RGB.R] = r;
            matrix[x, y, (int)RGB.G] = g;
            matrix[x, y, (int)RGB.B] = b;
        }

        // TODO: implement POST http request 

        // TODO: implement conversion from arrays into JSON

    }
}
