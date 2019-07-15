BlockHasher v1.0.0
==================
by taizou
https://github.com/tzlion/blockhasher
http://hhug.me
cactusgunman@gmail.com

A tool for Windows that will produce MD5 hashes for equally sized blocks within a file.

(Requires .net framework 3.5)

Made to assist in ROM dumping, especially identifying blocks of repeated data and areas
of difference between dumps. You may find it useful for something else?

Usage
-----
In the "BLOCK SIZE IN HEX" box, enter the block size in bytes, in hexadecimal.
The default is 100000, equivalent to 1048576 in decimal, which is equal to 1 megabyte.
Then, drag and drop a file or multiple files to the "drop a file here" area.
A list of hashes of the blocks of the specified size will appear in the left pane.
