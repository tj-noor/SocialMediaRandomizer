# Social Media Randomizer

## CSV Randomizer

### Create a CSV file with all the entries and another one with the type of entries supported along with its weight and let this randomizer create a weighted list and then shuffle it for as many times as you want and finally pick a winner!

#### Every shuffle is done by ordering the list with a Guid, ensuring a good randomized list.


```bash
Options:
  -?|-h|--help             Show help information
  -e|--entries <filePath>  This is the path where the CSV file with the entries are located.
  -c|--config <filePath>   This is the path where the CSV file with the configuration for entry types & weights are
                           located.
  -s|--shuffle <count>     This is number of times the list will be shuffled before an entry is chosen. Default is
                           10.
```