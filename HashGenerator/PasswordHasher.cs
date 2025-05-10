using System;
using BCrypt.Net;

public class HashGenerator
{
    public static void Main(string[] args)
    {
        string newAdminPassword = "admin"; // Thay bằng mật khẩu mới bạn muốn
        string newAdminHash = BCrypt.Net.BCrypt.HashPassword(newAdminPassword, 11);
        Console.WriteLine($"New Admin Hash: {newAdminHash}"); // Copy chuỗi hash này
    }
}