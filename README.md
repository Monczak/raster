# raster
A tool to generate brightness maps for the RAST3R robot

### Usage
###### This is a command line tool. You will accomplish bugger all if you run it by itself. 

Navigate to raster's location in the command line, and then type raster (path to image to convert). The tool will automatically convert the image to an RTF file, created in the same location as the original image, and show a little preview of the result in the command prompt. This RTF is later to be sent over to RAST3R's EV3 brick and then interpreted.

**Note:** This program was written very quickly, and might not be ready for actual use. My apologies.

### Command line
`raster <path to image> [color depth] [width]`

*<> - required, [] - optional*
