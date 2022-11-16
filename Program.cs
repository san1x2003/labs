using System;
using System.IO;

namespace C_APP
{


	class Program
	{
		public class Calc
		{
			
			abstract class Operation
			{
				public abstract float Eval();
			}

			
			class Number : Operation
			{
				public Number(float f)
				{ 
					value = f; 
				}
				public override float Eval() 
				{ 
					return value;
				}

				private float value;
			}

			
			abstract class Unary : Operation
			{
				public Unary(Operation op) 
				{ 
					one = op; 
				}

				protected Operation one;
			}

			
			abstract class Binary : Operation
			{
				public Binary(Operation l, Operation r) 
				{ 
					left = l; right = r;
				}

				protected Operation left, right;
			}

			
			class Negation : Unary
			{
				public Negation(Operation n) : base(n) 
				{ 
				
				}
				public override float Eval() 
				{ 
					return -one.Eval(); 
				}
			}

			
			class Plus : Binary
			{
				public Plus(Operation l, Operation r) : base(l, r)
				{ 
				
				}
				public override float Eval() 
				{
					return left.Eval() + right.Eval(); 
				}
			}

		
			class Minus : Binary
			{
				public Minus(Operation l, Operation r) : base(l, r) 
				{ 
				
				}
				public override float Eval() 
				{ 
					return left.Eval() - right.Eval(); 
				}
			}

			
			class Multiply : Binary
			{
				public Multiply(Operation l, Operation r) : base(l, r) 
				{ 
				
				}
				public override float Eval() 
				{
					return left.Eval() * right.Eval(); 
				}
			}

			
			class Divide : Binary
			{
				public Divide(Operation l, Operation r) : base(l, r) 
				{ 
				
				}
				public override float Eval()
				{
					float right_eval = right.Eval();
					if (right_eval == 0.0f)
					{
						System.Console.WriteLine("Devide by zero");
					}	
					return (right_eval != 0.0f) ? (left.Eval() / right_eval) : float.MaxValue;
				}
			}

			class Expression
			{
				public Expression(string s) 
				{ 
					source = s; 
				}

				public float Calc()
				{
					pos = 0;
					Operation root = Parse0();
					return (root != null) ? root.Eval() : 0.0f;
				}

				
				private Operation Parse0()
				{
					Operation result = Parse1();

					for ( ; ; )
					{
						if (Match('+'))
						{
							result = new Plus(result, Parse1());
						}
						else if (Match('-'))
						{
							result = new Minus(result, Parse1());
						}
						else
						{ 
							return result; 
						}
							
					}
				}

				
				private Operation Parse1()
				{
					Operation result = Parse2();
					for (; ; )
					{
						if (Match('*'))
						{
							result = new Multiply(result, Parse2());
						}
						else if (Match('/'))
						{
							result = new Divide(result, Parse2());
						}
						else 
						{ 
							return result;
						} 
					}
				}

				
				private Operation Parse2()
				{
					Operation result = null;
					
					if (Match('-'))
					{
						result = new Negation(Parse0());
					}
					else if (Match('('))
					{
						result = Parse0();
						if (!Match(')'))
						{
							System.Console.WriteLine("Missing ')'");
						}		
					}
					else
					{
						
						float val = 0.0f;
						int start = pos;
						while (pos < source.Length && (char.IsDigit(source[pos]) || source[pos] == '.' || source[pos] == 'e'))
						{ 
							++pos;
						}
						

						try 
						{ 
							val = float.Parse(source.Substring(start, pos - start)); 
						}
						catch
						{ 
							System.Console.WriteLine("Can't parse '" + source.Substring(start) + "'");
						}
						result = new Number(val);

					}
					return result;
				}

				
				private bool Match(char ch)
				{
					if (pos >= source.Length) 
					{
						return false;
					}
					
					while (source[pos] == ' ')
					{
						++pos;             
					}


					if (source[pos] == ch)
					{
						++pos;
						return true; 
					}
					else
					{
						return false;
					}
				}

				private string source;     
				private int pos;        
			}

			public static void Main()
			{
				while (true)
				{
					System.Console.Clear();
					System.Console.WriteLine("Ведите пример:");
					string buf = System.Console.ReadLine();

					Expression expr = new Expression(buf);
					Console.WriteLine("Решение: ");
					System.Console.WriteLine(expr.Calc().ToString("g"));
					System.Console.ReadKey();
				}

				
			}
		}
	}
}
