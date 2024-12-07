def read_matrix_from_file(file_path):
    matrix = []
    with open(file_path, 'r') as file:
        for line in enumerate(file, start=1):
            clean_line = line.strip().upper()
            matrix.append(list(clean_line))
    return matrix


def find_xmas(matrix, word="XMAS"):
    rows, cols = len(matrix), len(matrix[0])
    word_length = len(word)
    word_reverse = word[::-1]
    counter = 0

    for r in range(rows):
        row_string = ''.join(matrix[r])
        idx = row_string.find(word)
        while idx != -1:
            counter += 1
            idx = row_string.find(word, idx + 1)
        idx_rev = row_string.find(word_reverse)
        while idx_rev != -1:
            counter += 1
            idx_rev = row_string.find(word_reverse, idx_rev + 1)

    for c in range(cols):
        col_string = ''.join(matrix[r][c] for r in range(rows))
        idx = col_string.find(word)
        while idx != -1:
            counter += 1
            idx = col_string.find(word, idx + 1)
        idx_rev = col_string.find(word_reverse)
        while idx_rev != -1:
            counter += 1
            idx_rev = col_string.find(word_reverse, idx_rev + 1)

    for r in range(rows - word_length + 1):
        for c in range(cols - word_length + 1):
            diag = ''.join(matrix[r + i][c + i] for i in range(word_length))
            if diag == word:
                counter += 1
            if diag == word_reverse:
                counter += 1

    for r in range(word_length - 1, rows):
        for c in range(cols - word_length + 1):
            diag = ''.join(matrix[r - i][c + i] for i in range(word_length))
            if diag == word:
                counter += 1
            if diag == word_reverse:
                counter += 1

    return counter

def find_cross_patterns(matrix):
    rows, cols = len(matrix), len(matrix[0])
    cross_positions = []

    for r in range(1, rows - 1): 
        for c in range(1, cols - 1):  

            if (matrix[r][c] == "A"):
                if (
                    matrix[r - 1][c - 1] == "M" and      
                    matrix[r + 1][c - 1] == "M" and   
                    matrix[r - 1][c + 1] == "S" and 
                    matrix[r + 1][c + 1] == "S"
                ):
                    cross_positions.append((r, c))
                elif (
                    matrix[r][c] == "A" and      
                    matrix[r - 1][c - 1] == "S" and     
                    matrix[r + 1][c - 1] == "S" and     
                    matrix[r - 1][c + 1] == "M" and     
                    matrix[r + 1][c + 1] == "M"     
                ):
                    cross_positions.append((r, c))

                elif (
                    matrix[r][c] == "A" and             
                    matrix[r - 1][c - 1] == "M" and      
                    matrix[r + 1][c + 1] == "S" and      
                    matrix[r - 1][c + 1] == "M" and     
                    matrix[r + 1][c - 1] == "S"      
                ):
                    cross_positions.append((r, c))
                elif (
                    matrix[r][c] == "A" and              
                    matrix[r - 1][c - 1] == "S" and 
                    matrix[r + 1][c + 1] == "M" and      
                    matrix[r - 1][c + 1] == "S" and     
                    matrix[r + 1][c - 1] == "M"      
                ):
                    cross_positions.append((r, c))

    return cross_positions


def main():
    file_path = "test.txt" 
    matrix = read_matrix_from_file(file_path)

    matches = find_xmas(matrix)
    matchesXmas = find_cross_patterns(matrix)
    print(f"Total occurrences of 'XMAS': {matches}")
    print(f"Total occurrences of 'X-MAS': {len(matchesXmas)}")


if __name__ == "__main__":
    main()
