
file_path = "test.txt"

def is_valid_row(row):
    return (all(1 <= row[i] - row[i-1] <= 3 for i in range(1, len(row))) or 
            all(-3 <= row[i] - row[i-1] <= -1 for i in range(1, len(row))))

def find_valid_rows(file_path):
    valid_rows = []
    invalid_rows = []
    with open(file_path, 'r') as file:
        for row in map(lambda line: list(map(int, line.split())), file):
            if is_valid_row(row):
                valid_rows.append(row)
            else :invalid_rows.append(row)
    return valid_rows, invalid_rows

def find_modifierable_valid_rows(invalid_rows):
    convertible_rows = []

    for row in invalid_rows:
        for i in range(len(row)):
            new_row = row[:i] + row[i+1:]
            if is_valid_row(new_row):
                convertible_rows.append((row, i, new_row))  
                break 

    return convertible_rows

safe_rows, unsafe_row = find_valid_rows(file_path)
modified_safe_rows = find_modifierable_valid_rows(unsafe_row)
print("Safe Rows:", len(safe_rows))
print("Unsafe Rows:", len(unsafe_row))

print("Convertable unsafe Rows:", len(modified_safe_rows) + len(safe_rows))