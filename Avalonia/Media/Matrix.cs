// -----------------------------------------------------------------------
// <copyright file="Matrix.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;

    public struct Matrix : IFormattable
    {
        private double m11;
        private double m12;
        private double m21;
        private double m22;
        private double offsetX;
        private double offsetY;

        public Matrix(
            double m11,
            double m12,
            double m21,
            double m22,
            double offsetX,
            double offsetY)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public static Matrix Identity
        {
            get { return new Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0); }
        }

        public double Determinant
        {
            get { return (this.m11 * this.m22) - (this.m12 * this.m21); }
        }

        public bool HasInverse
        {
            get { return this.Determinant != 0; }
        }

        public bool IsIdentity
        {
            get { return this.Equals(Matrix.Identity); }
        }

        public double M11
        {
            get { return this.m11; }
            set { this.m11 = value; }
        }

        public double M12
        {
            get { return this.m12; }
            set { this.m12 = value; }
        }

        public double M21
        {
            get { return this.m21; }
            set { this.m21 = value; }
        }

        public double M22
        {
            get { return this.m22; }
            set { this.m22 = value; }
        }

        public double OffsetX
        {
            get { return this.offsetX; }
            set { this.offsetX = value; }
        }

        public double OffsetY
        {
            get { return this.offsetY; }
            set { this.offsetY = value; }
        }

        public static bool Equals(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }

        public static Matrix Multiply(Matrix trans1, Matrix trans2)
        {
            Matrix m = trans1;
            m.Append(trans2);
            return m;
        }

        public static Matrix Parse(string source)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Equals(matrix2);
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(matrix2);
        }

        public static Matrix operator *(Matrix trans1, Matrix trans2)
        {
            Matrix result = trans1;
            result.Append(trans2);
            return result;
        }

        public void Append(Matrix matrix)
        {
            double m11;
            double m21;
            double m12;
            double m22;
            double offsetX;
            double offsetY;

            m11 = (this.m11 * matrix.M11) + (this.m12 * matrix.M21);
            m12 = (this.m11 * matrix.M12) + (this.m12 * matrix.M22);
            m21 = (this.m21 * matrix.M11) + (this.m22 * matrix.M21);
            m22 = (this.m21 * matrix.M12) + (this.m22 * matrix.M22);

            offsetX = (this.offsetX * matrix.M11) + ((this.offsetY * matrix.M21) + matrix.OffsetX);
            offsetY = (this.offsetX * matrix.M12) + ((this.offsetY * matrix.M22) + matrix.OffsetY);

            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public bool Equals(Matrix value)
        {
            return this.m11 == value.M11 &&
                   this.m12 == value.M12 &&
                   this.m21 == value.M21 &&
                   this.m22 == value.M22 &&
                   this.offsetX == value.OffsetX &&
                   this.offsetY == value.OffsetY;
        }

        public override bool Equals(object o)
        {
            if (!(o is Matrix))
            {
                return false;
            }

            return this.Equals((Matrix)o);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void Invert()
        {
            if (!this.HasInverse)
            {
                throw new InvalidOperationException("Transform is not invertible.");
            }

            double d = this.Determinant;

            double m11 = this.m22;
            double m12 = -this.m12;
            double m21 = -this.m21;
            double m22 = this.m11;
            double offsetX = (this.m21 * this.offsetY) - (this.m22 * this.offsetX);
            double offsetY = (this.m12 * this.offsetX) - (this.m11 * this.offsetY);

            this.m11 = m11 / d;
            this.m12 = m12 / d;
            this.m21 = m21 / d;
            this.m22 = m22 / d;
            this.offsetX = offsetX / d;
            this.offsetY = offsetY / d;
        }

        public void Prepend(Matrix matrix)
        {
            double m11;
            double m21;
            double m12;
            double m22;
            double offsetX;
            double offsetY;

            m11 = (matrix.M11 * this.m11) + (matrix.M12 * this.m21);
            m12 = (matrix.M11 * this.m12) + (matrix.M12 * this.m22);
            m21 = (matrix.M21 * this.m11) + (matrix.M22 * this.m21);
            m22 = (matrix.M21 * this.m12) + (matrix.M22 * this.m22);

            offsetX = (matrix.OffsetX * this.m11) + ((matrix.OffsetY * this.m21) + this.offsetX);
            offsetY = (matrix.OffsetX * this.m12) + ((matrix.OffsetY * this.m22) + this.offsetY);

            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public void Rotate(double angle)
        {
            double theta = angle * Math.PI / 180;

            Matrix r_theta = new Matrix(
                Math.Cos(theta),
                Math.Sin(theta),
                -Math.Sin(theta),
                Math.Cos(theta),
                0,
                0);

            this.Append(r_theta);
        }

        public void RotateAt(double angle, double centerX, double centerY)
        {
            this.Translate(-centerX, -centerY);
            this.Rotate(angle);
            this.Translate(centerX, centerY);
        }

        public void RotateAtPrepend(double angle, double centerX, double centerY)
        {
            Matrix m = Matrix.Identity;
            m.RotateAt(angle, centerX, centerY);
            this.Prepend(m);
        }

        public void RotatePrepend(double angle)
        {
            Matrix m = Matrix.Identity;
            m.Rotate(angle);
            this.Prepend(m);
        }

        public void Scale(double scaleX, double scaleY)
        {
            Matrix scale = new Matrix(scaleX, 0, 0, scaleY, 0, 0);
            this.Append(scale);
        }

        public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY)
        {
            this.Translate(-centerX, -centerY);
            this.Scale(scaleX, scaleY);
            this.Translate(centerX, centerY);
        }

        public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY)
        {
            Matrix m = Matrix.Identity;
            m.ScaleAt(scaleX, scaleY, centerX, centerY);
            this.Prepend(m);
        }

        public void ScalePrepend(double scaleX, double scaleY)
        {
            Matrix m = Matrix.Identity;
            m.Scale(scaleX, scaleY);
            this.Prepend(m);
        }

        public void SetIdentity()
        {
            this.m11 = this.m22 = 1.0;
            this.m12 = this.m21 = 0.0;
            this.offsetX = this.offsetY = 0.0;
        }

        public void Skew(double skewX, double skewY)
        {
            Matrix skew_m = new Matrix(
                1,
                Math.Tan(skewY * Math.PI / 180),
                Math.Tan(skewX * Math.PI / 180),
                1,
                0,
                0);
            this.Append(skew_m);
        }

        public void SkewPrepend(double skewX, double skewY)
        {
            Matrix m = Matrix.Identity;
            m.Skew(skewX, skewY);
            this.Prepend(m);
        }

        public override string ToString()
        {
            if (this.IsIdentity)
            {
                return "Identity";
            }
            else
            {
                return string.Format(
                    "{0},{1},{2},{3},{4},{5}",
                    this.m11,
                    this.m12,
                    this.m21,
                    this.m22,
                    this.offsetX,
                    this.offsetY);
            }
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public Point Transform(Point point)
        {
            return Point.Multiply(point, this);
        }

        public void Transform(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = this.Transform(points[i]);
            }
        }

        public Vector Transform(Vector vector)
        {
            return Vector.Multiply(vector, this);
        }

        public void Transform(Vector[] vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = this.Transform(vectors[i]);
            }
        }

        public void Translate(double offsetX, double offsetY)
        {
            this.offsetX += offsetX;
            this.offsetY += offsetY;
        }

        public void TranslatePrepend(double offsetX, double offsetY)
        {
            Matrix m = Matrix.Identity;
            m.Translate(offsetX, offsetY);
            this.Prepend(m);
        }

        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}