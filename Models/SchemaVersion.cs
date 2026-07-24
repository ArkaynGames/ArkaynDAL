using System;
using System.Collections.Generic;
using System.Text;

namespace ArkaynDAL.Models
{
    public class SchemaVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }

        // Build = YYWW (Year + Week)
        public int Build { get; set; }

        public SchemaVersion() { }

        public SchemaVersion(int major, int minor, int build)
        {
            Major = major;
            Minor = minor;
            Build = build;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Build}";
        }

        public static SchemaVersion Parse(string versionString)
        {
            if (string.IsNullOrWhiteSpace(versionString))
                throw new ArgumentException("Version string cannot be null or empty.");

            var parts = versionString.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Version string must be in format Major.Minor.Build");

            return new SchemaVersion(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            );
        }

        public bool IsNewerThan(SchemaVersion other)
        {
            if (other == null) return true;

            if (Major != other.Major)
                return Major > other.Major;

            if (Minor != other.Minor)
                return Minor > other.Minor;

            return Build > other.Build;
        }

        public bool Equals(SchemaVersion other)
        {
            if (other == null) return false;

            return Major == other.Major &&
                   Minor == other.Minor &&
                   Build == other.Build;
        }
    }
}

