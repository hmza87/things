Module Module1
    Private key1 As String = "KEY1"
    Private key2 As String = "KEY2"
    Public Function CodiTwice(txt As String) As String
        Return Codi(Codi(txt, key1), key2)
    End Function
    Public Function DCodiTwice(txt As String) As String
        Return DCodi(DCodi(txt, key1), key2)
    End Function

    Public Function Codi(ByVal TXT As String, ByVal LKEY As String) As String

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.Encoding.UTF8.GetBytes(LKEY))

            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(TXT)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
        End Try
    End Function

    Public Function DCodi(ByVal TXT As String, ByVal LKEY As String) As String

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.Encoding.UTF8.GetBytes(LKEY))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(TXT)
            decrypted = System.Text.Encoding.UTF8.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
        End Try
    End Function
End Module
