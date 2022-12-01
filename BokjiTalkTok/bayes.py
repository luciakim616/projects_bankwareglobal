import pandas as pd
import torch
import numpy as np

distribution_tensor = torch.load("distribution_tensor.dat")
noun_idx = torch.load("noun_idx.dat")
noun_len = distribution_tensor.shape[1]
noun_bin = [key for key, _ in noun_idx.items()]

#print(noun_idx)
def bayes(query, verbose = True):
    num_ten = torch.zeros(1, noun_len)
    for n in noun_bin:
        if n in query:
            num_ten[0, noun_idx[n]] += 1
    #print(num_ten)
    evals = distribution_tensor@num_ten.T
    #print(evals)
    idx = torch.argmax(evals)
    val = idx.item()
    name, target = dt[['Name', 'Target']].values[val]
    ret =  f'추천드리는건 {val} 번째 정책 :' + name.replace('\n', ' ') + target.replace('\n', ' ')
    return ret
    

dt = pd.read_csv("dataexport.csv")
df = dt[['Category1', 'Category2', 'Category3']].values
