JsonToCsvMerge is a quick and dirty tool to merge multiple files each containing multiple Json entries into one big CSV file.
The current implementation is capable of detecting nested objects (1 layer deep) that have been nested by using {} curly brackets (not the [] ones as it would be normally used).
Json files not containing any nested content will run just fine.

Usage:

Use Cmd to start. Argument 1: Path to the highest level folder that contains all Json files to be merged.

