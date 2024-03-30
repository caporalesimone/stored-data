# Stored Data
StoredData provides a simple way to store and retrieve data in a JSON file. 

This project is a POC for a library that I want to use into my Ultima Online™ RazorEnhanced [Scripts](https://github.com/caporalesimone/RazorEnhacedCSharpScripts).

It allows you to store data globally, per server, and per character.


## Usage

### Initialization:
To start using StoredData, you need to initialize it by creating an instance of the `StoredData` class. You need to provide the path to the folder where the JSON file will be stored, the name of the server, and the name of the character.

```csharp
// Initialize StoredData
StoredData storedData = new StoredData("path/to/folder", "serverName", "characterName");
```

### Storing Data:
You can store data using the `StoreData` method. You need to provide the data, the key name, and the storage type (Global, Server, or Character).

```csharp
// Storing data globally
string data = "Hello, world!";
string key = "greeting";
storedData.StoreData(data, key, StoredData.StoreType.Global);
```

```csharp
// Storing data per server
string data = "Hello, server!";
string key = "greeting";
storedData.StoreData(data, key, StoredData.StoreType.Server);
```

### Retrieving Data:
You can retrieve data using the `GetData` method. You need to provide the key name and the storage type (Global, Server, or Character). The method will return the data stored with the specified key name and storage type.

```csharp
public class Email
{
    public string email { get; set; }
    public string password { get; set; }
    public string smtp { get; set; }

    public Email() {}
}

[...]

email = data.GetData<Email>("email", StoredData.StoreType.Character);
```

### JSON Example:
Here is an example of the JSON structure that is generated:

```json
{
  "Global": {
    "integer_global": 12
  },
  "Servers": {
    "UOAlive": {
      "ServerGlobal": {
        "integer_server": 13,
        "list": [
          1,
          2,
          3
        ]
      },
      "Characters": {
        "Test Character": {
          "integer_character": 14,
          "email": {
            "email": "testmail@gmail.com",
            "password": "testpassword",
            "smtp": "smtp.gmail.com"
          }
        }
      }
    }
  }
}
```

## JSON Schema
The [JSON schema](Schema.json) for the StoredData structure is as follows:

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "StoredData Schema",
  "type": "object",
  "properties": {
    "Global": {
      "type": "object",
      "additionalProperties": true
    },
    "Servers": {
      "type": "object",
      "additionalProperties": {
        "type": "object",
        "properties": {
          "ServerGlobal": {
            "type": "object",
            "additionalProperties": true
          },
          "Characters": {
            "type": "object",
            "additionalProperties": {
              "type": "object",
              "additionalProperties": true
            }
          }
        },
        "required": ["ServerGlobal", "Characters"],
        "additionalProperties": false
      }
    }
  },
  "required": ["Global", "Servers"],
  "additionalProperties": false
}
```

## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE License. See the [LICENSE](LICENSE) file for details.
