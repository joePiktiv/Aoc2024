
def read_input_from_file(file_path):
    with open(file_path, 'r') as file:
        lines = file.read().strip().split('\n')
    separator_index = lines.index("")  
    rules = []
    for line in lines[:separator_index]: 
        before, after = map(int, line.split('|'))
        rules.append((before, after))
    printed_pages_lines = [list(map(int, line.split(','))) for line in lines[separator_index + 1:]]
    
    return rules, printed_pages_lines

    
def check_print_order(rules, printed_pages):
    printed_pages_set = set(printed_pages)  
    
    for before, after in rules:
        if before in printed_pages_set and after in printed_pages_set:
            before_index = printed_pages.index(before)
            after_index = printed_pages.index(after)
            if before_index > after_index:
                return False 
    return True

def get_middle_values_sum(printed_pages):
    n = len(printed_pages)
    middle_index = n // 2
    middle_values = printed_pages[middle_index]
    return middle_values

def rearrange_pages_based_on_rules(rules, printed_pages):

    def is_order_valid():
        return check_print_order(rules, printed_pages)
    
    while not is_order_valid():
        for before, after in rules:
            if before in printed_pages and after in printed_pages:
                before_index = printed_pages.index(before)
                after_index = printed_pages.index(after)
                if before_index > after_index:
                    printed_pages[before_index], printed_pages[after_index] = printed_pages[after_index], printed_pages[before_index]
                    break 
    return printed_pages


def main():
    file_path = "test.txt" 

    rules, printed_pages_lines = read_input_from_file(file_path)
    total_valid = 0
    total_invalid = 0
    for i, printed_pages in enumerate(printed_pages_lines):
        if check_print_order(rules, printed_pages):
            total_valid += get_middle_values_sum(printed_pages)
        else:
            rearranged_order = rearrange_pages_based_on_rules(rules, printed_pages)
            median_value = get_middle_values_sum(rearranged_order)
            total_invalid += median_value
    print("middle valid sum ", total_valid)
    print("middle invalid sum ", total_invalid)

if __name__ == "__main__":
    main()