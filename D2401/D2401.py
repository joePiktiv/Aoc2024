list1 = []
list2 = []

file_path = "./test.txt"

with open(file_path, 'r') as file:
    for line in file:
        num1, num2 = map(int, line.split('   '))
        list1.append(num1)
        list2.append(num2)

list1_sorted = sorted(list1)
list2_sorted = sorted(list2)

total_difference = sum(abs(a - b) for a, b in zip(sorted(list1), sorted(list2)))
print("Total Difference Sum:", total_difference)

total_sum = sum(list2.count(value) * value for value in set(list1))
print("Total Sum of Products:", total_sum)
