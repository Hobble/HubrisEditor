using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubrisEditor.Core
{
    public class PerlinNoise
    {
        public PerlinNoise(int seed)
        {
            m_random = new Random(seed);
            InitGradients();
        }

        public float Noise(float x, float y, float z)
        {
            int ix = (int)Math.Floor(x);
            float fx0 = x - ix;
            float fx1 = fx0 - 1;
            float wx = Smooth(fx0);

            int iy = (int)Math.Floor(y);
            float fy0 = y - iy;
            float fy1 = fy0 - 1;
            float wy = Smooth(fy0);

            int iz = (int)Math.Floor(z);
            float fz0 = z - iz;
            float fz1 = fz0 - 1;
            float wz = Smooth(fz0);

            float vx0 = Lattice(ix, iy, iz, fx0, fy0, fz0);
            float vx1 = Lattice(ix + 1, iy, iz, fx1, fy0, fz0);
            float vy0 = Lerp(wx, vx0, vx1);

            vx0 = Lattice(ix, iy + 1, iz, fx0, fy1, fz0);
            vx1 = Lattice(ix + 1, iy + 1, iz, fx1, fy1, fz0);
            float vy1 = Lerp(wx, vx0, vx1);

            float vz0 = Lerp(wy, vy0, vy1);

            vx0 = Lattice(ix, iy, iz + 1, fx0, fy0, fz1);
            vx1 = Lattice(ix + 1, iy, iz + 1, fx1, fy0, fz1);
            vy0 = Lerp(wx, vx0, vx1);

            vx0 = Lattice(ix, iy + 1, iz + 1, fx0, fy1, fz1);
            vx1 = Lattice(ix + 1, iy + 1, iz + 1, fx1, fy1, fz1);
            vy1 = Lerp(wx, vx0, vx1);

            float vz1 = Lerp(wy, vy0, vy1);
            return Lerp(wz, vz0, vz1);
        }

        private void InitGradients()
        {
            for (int i = 0; i < GradientSizeTable; i++)
            {
                float z = 1f - 2f * (float)m_random.NextDouble();
                float r = (float)Math.Sqrt(1f - z * z);
                float theta = 2 * (float)Math.PI * (float)m_random.NextDouble();
                m_gradients[i * 3] = r * (float)Math.Cos(theta);
                m_gradients[i * 3 + 1] = r * (float)Math.Sin(theta);
                m_gradients[i * 3 + 2] = z;
            }
        }

        private int Permutate(int x)
        {
            const int mask = GradientSizeTable - 1;
            return m_perm[x & mask];
        }

        private int Index(int ix, int iy, int iz)
        {
            return Permutate(ix + Permutate(iy + Permutate(iz)));
        }

        private float Lattice(int ix, int iy, int iz, float fx, float fy, float fz)
        {
            int index = Index(ix, iy, iz);
            int g = index * 3;
            return m_gradients[g] * fx + m_gradients[g + 1] * fy + m_gradients[g + 2] * fz;
        }

        private float Lerp(float t, float value0, float value1)
        {
            return value0 + t * (value1 - value0);
        }

        private float Smooth(float x)
        {
            return x * x * (3 - 2 * x);
        }

        public byte[] GetPixels(int height, int width)
        {
            byte[] pixels = new byte[height * width];
            float[] buffer = new float[height * width];
            float persistence = 0.5f;
            float min = float.MaxValue;
            float max = float.MinValue;
            float pmin = float.MaxValue;
            float pmax = float.MinValue;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float k = 0.0f;
                    float result = 0.0f;
                    do
                    {
                        float frequency = (float)Math.Pow(2.0, (float)k + 2.0f);
                        float amplitude = (float)Math.Pow(persistence, (float)k + 1.0f);
                        float value = Noise((frequency * j) / width, (frequency * i) / height, 0.0f);
                        value = Math.Max(Math.Min(value + 0.5f, 1.0f), 0.0f);
                        result += (value * amplitude);
                        k += 1;
                    } while (k < 6);
                    if (result > max)
                    {
                        max = result;
                    }
                    if (result < min)
                    {
                        min = result;
                    }
                    buffer[i * width + j] = result;
                }
            }

            float range = (max - min) / 2.0f;
            float scale = 0.5f / range;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float result = buffer[i * width + j];

                    result -= min;
                    result -= range;
                    result *= scale;
                    result += 0.5f;

                    if (result > pmax)
                    {
                        pmax = result;
                    }
                    if (result < pmin)
                    {
                        pmin = result;
                    }

                    pixels[i * width + j] = (byte)(result * 255);
                }
            }

            return pixels;
        }

        private const int GradientSizeTable = 256;
        private readonly Random m_random;
        private readonly float[] m_gradients = new float[GradientSizeTable * 3];

        private readonly byte[] m_perm = new byte[]
        {
              225, 155, 210, 108, 175, 199, 221, 144, 203, 116,  70, 213,  69, 158,  33, 252,
                5,  82, 173, 133, 222, 139, 174,  27,   9,  71,  90, 246,  75, 130,  91, 191,
              169, 138,   2, 151, 194, 235,  81,   7,  25, 113, 228, 159, 205, 253, 134, 142,
              248,  65, 224, 217,  22, 121, 229,  63,  89, 103,  96, 104, 156,  17, 201, 129,
               36,   8, 165, 110, 237, 117, 231,  56, 132, 211, 152,  20, 181, 111, 239, 218,
              170, 163,  51, 172, 157,  47,  80, 212, 176, 250,  87,  49,  99, 242, 136, 189,
              162, 115,  44,  43, 124,  94, 150,  16, 141, 247,  32,  10, 198, 223, 255,  72,
               53, 131,  84,  57, 220, 197,  58,  50, 208,  11, 241,  28,   3, 192,  62, 202,
               18, 215, 153,  24,  76,  41,  15, 179,  39,  46,  55,   6, 128, 167,  23, 188,
              106,  34, 187, 140, 164,  73, 112, 182, 244, 195, 227,  13,  35,  77, 196, 185,
               26, 200, 226, 119,  31, 123, 168, 125, 249,  68, 183, 230, 177, 135, 160, 180,
               12,   1, 243, 148, 102, 166,  38, 238, 251,  37, 240, 126,  64,  74, 161,  40,
              184, 149, 171, 178, 101,  66,  29,  59, 146,  61, 254, 107,  42,  86, 154,   4,
              236, 232, 120,  21, 233, 209,  45,  98, 193, 114,  78,  19, 206,  14, 118, 127,
               48,  79, 147,  85,  30, 207, 219,  54,  88, 234, 190, 122,  95,  67, 143, 109,
              137, 214, 145,  93,  92, 100, 245,   0, 216, 186,  60,  83, 105,  97, 204,  52
        };
    }
}
