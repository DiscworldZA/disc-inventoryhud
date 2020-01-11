# [ESX] [DISC] disc-inventoryhud
The ACTUAL real code for the inventoryhud that is not broken

# ESX Compatibility

This is a fork for use with the inventory [Rex's ESX](https://github.com/rex2630/es_extended)

# Events

## Add Item Event

```lua
TriggerEvent('disc-inventoryhud:addItem', source, name, count)
```

## Remove Item Event
### Can only be used in Item Use Callback
```lua
TriggerEvent('disc-inventoryhud:removeItem', source, item.Id, 1, item.Slot, item.Inventory)
```

## Register Item for Use

```lua
TriggerEvent('disc-inventoryhud:registerItemUse', item, cb)
```

### Example (Courtesy of esx_basicneeds)

```lua
TriggerEvent('disc-inventoryhud:registerItemUse', "water", function(source, item)
	local xPlayer = ESX.GetPlayerFromId(source)
	TriggerEvent('disc-inventoryhud:removeItem', source, item.Id, 1, item.Slot, item.Inventory)

	TriggerClientEvent('esx_status:add', source, 'thirst', 200000)
	TriggerClientEvent('esx_basicneeds:onDrink', source)
	TriggerClientEvent('esx:showNotification', source, _U('used_water'))
end)
```

## Check Has Item
```lua
TriggerEvent('disc-inventoryhud:hasItem', source, 'bread', function(hasItem)
    if hasItem then
        print('Has Item')
    else
        print('No Item')
    end
end)
```
# REQUIREMENTS
disc-base [repo](https://github.com/DiscworldZA/gta-resources)

# BETA

This version contains everything besides shops and weapons. 

# Stream

I stream on [Twitch](https://www.twitch.tv/DiscworldZA). Come hang out and learn from me!

# Other Downloads

Other Downloads available on my [repo](https://github.com/DiscworldZA/gta-resources)
