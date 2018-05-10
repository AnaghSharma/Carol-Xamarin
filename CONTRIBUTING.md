## Tools required
1. Visual Studio for Mac
2. Xcode

Updated versions preferred

## Steps
1. Clone this repo and open the project in Visual Studio for Mac
2. Register your application at [Musixmatch](developer.musixmatch.com). You will get an API Key after registration.
3. Add a new JSON file at the root of your project and name it as `Secrets.json`.
4. Add following key value pair in `Secrets.json`

   `{
      "API_key": "your secret api_key"
    }`
5. Save. Build and run the solution. It should work without any errors.

## macOS app
For additional help in understanding how to build macOS menu bar app using C# and Xamarin, please check out [this project](https://github.com/AnaghSharma/Ambar)
