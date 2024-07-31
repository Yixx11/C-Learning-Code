#include <iostream>
#include <unordered_map>
using namespace std;

class Solution {
public:
    int lengthOfLongestSubstring(string s)
    {
        size_t  StrLength = s.length();
        char CurrentMaxLong[8] = { 0 };
        int index = 0;
        int count = 0;
        for (int i = 0; i < StrLength; i++)
        {
            for (int m = 0; m < StrLength; m++)
            {
                if (s[i] != CurrentMaxLong[m])
                {
                    if (m == index)
                    {
                        CurrentMaxLong[index] = s[i];
                        index += 1;
                        if (index >= count)
                        {
                            count = index;
                        }                        
                        break;
                    }
                }
                else
                {
                    memset(CurrentMaxLong, 0, 8);
                    CurrentMaxLong[0] = s[i];
                    index = 1;
                    break;
                }
            }
        }
        return count;
    }
};

class Solution2 {
public:
    int lengthOfLongestSubstring(string s)
    {
        if (s.length() == 0)
            return 0;
        std::unordered_map<char, int> hashTable;
        int maxLength = 0;
        int currentLength = 0;
        int startIndex = 0;

        for (int i = 0; i < s.length(); i++)
        {
            if (hashTable.find(s[i]) == hashTable.end())
            {
                currentLength++;
                hashTable[s[i]] = i;
            }
            else
            {
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                }
                startIndex = max(hashTable[s[i]], startIndex);
                currentLength = i - startIndex;
                hashTable[s[i]] = i;
            }
        }
        if (currentLength > maxLength)
        {
            maxLength = currentLength;
        }
        return maxLength;
    }

};

int Maxnum = 0;
int main() 
{

    string STR;
    cout << "请输入字符串：" << endl;
    cin >> STR;


    Solution2 soul;
    Maxnum = soul.lengthOfLongestSubstring(STR);
    cout << "最大的长度为：" << Maxnum << endl;

	system("pause");
	return 0;
}