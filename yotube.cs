import urllib.request
import pafy
from bs4 import BeautifulSoup
from tqdm import tqdm
 
inp = input("Plz Enter Url Youtube Playlist ")
 
ur = urllib.request.urlopen(inp).read()
 
 
soup = BeautifulSoup(ur,"html.parser")
list_Youtub = []
for i in soup.find_all("a"):
    s = i.get("href")
    if "index=" in s:
        if s in list_Youtub:
            pass
        else:
            list_Youtub.append("https://www.youtube.com"+s)
 
#print(len(list_Youtub))
#print(list_Youtub)
youtube_download = {}
 
 
for _y in list_Youtub:
    url = _y
    video = pafy.new(url)
    stream = video.streams
    for s in stream:
        if s.extension == "mp4" and "720" in s.resolution:
            #print(s.resolution, s.extension, s.get_filesize(), s.url)
            youtube_download.update({s.get_filesize():s.url})
 
 
 
 
print(youtube_download)
 
nf = 1
for key in youtube_download:
    urlopen = youtube_download[key]
    upen = urllib.request.urlopen(urlopen)
    file_open = open(str(nf)+".mp4","wb")
    sizeB = 500
    for do in tqdm(range(key)):
        buffer = upen.read(sizeB)
        do = do + sizeB
        file_open.write(buffer)
    nf = nf + 1
 
 
file_open.close()