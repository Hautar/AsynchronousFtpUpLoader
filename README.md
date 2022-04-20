# AsynchronousFtpUpLoader
Uploads a file from LocalPath  to an ftp server RemotePath every 30 seconds. Then file is moved from LocalPath to LocalPathDel directory.

Simple small example of how you can use FluentFtp to upload files to an ftp server with authorization.

Specify server URL, user login, password, local paths and remote paths in settings.json file.
settings.json is automatically generated if you delete it, but it will have default values on creation.
You can easily customize both settings class and settings file to your own purpose.
Best regards, Ratmir!
