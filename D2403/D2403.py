import re

def extract_mul_patterns_from_file(file_path):
    pattern = r"mul\((\d+),\s*(\d+)\)"
    total_sum = 0
    with open(file_path, 'r') as file:
        for line in file:
            for m in re.findall(pattern, line):
                total_sum += int(m[0]) * int(m[1])
    return total_sum

def extract_mul_patterns_from_file2(file_path):
    pattern_mul_do_dont = r"do\(\)|don't\(\)|mul\(\d+,\d+\)"
    
    total_sum = 0 
    add_mul = True  
    
    with open(file_path, 'r') as file:
        for line in file:
            matches = re.findall(pattern_mul_do_dont, line)
            for match in matches:
                if match == "don't()":  
                    add_mul = False  
                elif match == "do()":  
                    add_mul = True  
                elif add_mul:  
                    figure1, figure2 = map(int, match[4:-1].split(","))
                    total_sum += figure1 * figure2  
    return total_sum

file_path = 'test.txt'  
total1 = extract_mul_patterns_from_file(file_path)
total2 = extract_mul_patterns_from_file2(file_path)
print("Total 1 (without do/don't handling):", total1)
print("Total 2 (with do/don't handling):", total2)
