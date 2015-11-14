# Создание элемента <machineKey> с возможностью копирования и вставки в файл Web.config.
# Для приложений ASP.NET 4.0 достаточно запустить этот скрипт, чтобы создать элемент <machineKey>
# Вывод будет: <machineKey decryption="AES" decryptionKey="..." validation="HMACSHA256" validationKey="..." />
# Приложения ASP.NET 2.0 и 3.5 не поддерживают тип HMACSHA256. Вместо него можно указать параметр SHA1, чтобы создать совместимый элемент <machineKey>:
# Для этого добавить к Generate-MachineKey параметр -validation sha1, чтобы строка была такой
# Generate-MachineKey -validation sha1
# Созданный элемент <machineKey> можно вставить в файл Web.config. Элемент <machineKey> действителен только в файле Web.config в корневом каталоге приложения. На уровне вложенной папки он недействителен.
# Чтобы ознакомиться с полным списком поддерживаемых алгоритмов, измените последнюю строчку на help Generate-MachineKey.

function Generate-MachineKey {
  [CmdletBinding()]
  param (
    [ValidateSet("AES", "DES", "3DES")]
    [string]$decryptionAlgorithm = 'AES',
    [ValidateSet("MD5", "SHA1", "HMACSHA256", "HMACSHA384", "HMACSHA512")]
    [string]$validationAlgorithm = 'HMACSHA256'
  )
  process {
    function BinaryToHex {
        [CmdLetBinding()]
        param($bytes)
        process {
            $builder = new-object System.Text.StringBuilder
            foreach ($b in $bytes) {
              $builder = $builder.AppendFormat([System.Globalization.CultureInfo]::InvariantCulture, "{0:X2}", $b)
            }
            $builder
        }
    }
    switch ($decryptionAlgorithm) {
      "AES" { $decryptionObject = new-object System.Security.Cryptography.AesCryptoServiceProvider }
      "DES" { $decryptionObject = new-object System.Security.Cryptography.DESCryptoServiceProvider }
      "3DES" { $decryptionObject = new-object System.Security.Cryptography.TripleDESCryptoServiceProvider }
    }
    $decryptionObject.GenerateKey()
    $decryptionKey = BinaryToHex($decryptionObject.Key)
    $decryptionObject.Dispose()
    switch ($validationAlgorithm) {
      "MD5" { $validationObject = new-object System.Security.Cryptography.HMACMD5 }
      "SHA1" { $validationObject = new-object System.Security.Cryptography.HMACSHA1 }
      "HMACSHA256" { $validationObject = new-object System.Security.Cryptography.HMACSHA256 }
      "HMACSHA385" { $validationObject = new-object System.Security.Cryptography.HMACSHA384 }
      "HMACSHA512" { $validationObject = new-object System.Security.Cryptography.HMACSHA512 }
    }
    $validationKey = BinaryToHex($validationObject.Key)
    $validationObject.Dispose()
    [string]::Format([System.Globalization.CultureInfo]::InvariantCulture,
      "<machineKey decryption=`"{0}`" decryptionKey=`"{1}`" validation=`"{2}`" validationKey=`"{3}`" />",
      $decryptionAlgorithm.ToUpperInvariant(), $decryptionKey,
      $validationAlgorithm.ToUpperInvariant(), $validationKey)
  }
}

Generate-MachineKey
