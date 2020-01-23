fx_version 'adamant'

game 'gta5'

description 'Disc InventoryHud Redo'

version '0.0.1'

client_scripts {
    "disc_inventoryhud_common.net.dll",
    "disc_inventoryhud_client.net.dll",
    "Newtonsoft.Json.dll"
}

server_scripts {
    "disc_inventoryhud_common.net.dll",
    "disc_inventoryhud_server.net.dll",
    "Newtonsoft.Json.dll"
}

ui_page "web/html/index.html"

files { "web/html/main.js", "web/html/index.html", "Stashes.json" }
