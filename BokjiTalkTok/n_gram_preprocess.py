import pandas as pd
import torch
from konlpy.tag import Okt

df = pd.read_csv("dataexport.csv")
df = df[:-1]
okt=Okt()
noun_list = set()

for strings in list(df[['Name', 'DetailedInfo', 'SupportInfo', 'Target']].values):
    for ds in strings:
        try:
            lis = okt.nouns(ds)
            for noun in lis:
                noun_list.add(noun)
        except:
            continue

noun_len = len(noun_list)
            
noun_idx = {noun : idx for idx, noun in enumerate(noun_list)}
distribution_tensor = torch.zeros(df.values.shape[0], noun_len)

idx = 0

for strings in list(df[['Name', 'DetailedInfo', 'SupportInfo', 'Target']].values):
    for ds in strings:
        
        try:
            lis = okt.nouns(ds)
            for noun in lis:
                n_idx = noun_idx[noun]
                distribution_tensor[idx, n_idx] += 1
        except:
            continue

    idx += 1

distribution_tensor = distribution_tensor.float()/torch.sqrt(distribution_tensor.sum(dim = 1, keepdims = True))
print(distribution_tensor)

print( list(distribution_tensor.reshape(1, -1).tolist()))
torch.save(noun_idx, "noun_idx.dat")
torch.save(distribution_tensor, "distribution_tensor.dat")


    
    